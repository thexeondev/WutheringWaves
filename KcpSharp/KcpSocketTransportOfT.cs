using System;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace KcpSharp
{
    /// <summary>
    /// A Socket transport for upper-level connections.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class KcpSocketTransport<T> : IKcpTransport, IDisposable where T : class, IKcpConversation
    {
        private readonly UdpClient _udp;
        private readonly int _mtu;
        private T? _connection;
        private CancellationTokenSource? _cts;
        private bool _disposed;

        private int _handshakeSize;
        private Func<UdpReceiveResult, ValueTask>? _handshakeHandler;

        /// <summary>
        /// Construct a socket transport with the specified socket and remote endpoint.
        /// </summary>
        /// <param name="udp">The socket instance.</param>
        /// <param name="endPoint">The remote endpoint.</param>
        /// <param name="mtu">The maximum packet size that can be transmitted.</param>
        protected KcpSocketTransport(UdpClient udp, int mtu)
        {
            _udp = udp ?? throw new ArgumentNullException(nameof(udp));
            _mtu = mtu;
            if (mtu < 50)
            {
                throw new ArgumentOutOfRangeException(nameof(mtu));
            }
        }

        /// <summary>
        /// Get the upper-level connection instace. If Start is not called or the transport is closed, <see cref="InvalidOperationException"/> will be thrown.
        /// </summary>
        /// <exception cref="InvalidOperationException">Start is not called or the transport is closed.</exception>
        public T Connection => _connection ?? throw new InvalidOperationException();

        /// <summary>
        /// Create the upper-level connection instance.
        /// </summary>
        /// <returns>The upper-level connection instance.</returns>
        protected abstract T Activate();

        /// <summary>
        /// Allocate a block of memory used to receive from socket.
        /// </summary>
        /// <param name="size">The minimum size of the buffer.</param>
        /// <returns>The allocated memory buffer.</returns>
        protected virtual IMemoryOwner<byte> AllocateBuffer(int size)
        {
#if NEED_POH_SHIM
            return MemoryPool<byte>.Shared.Rent(size);
#else
            return new ArrayMemoryOwner(GC.AllocateUninitializedArray<byte>(size, pinned: true));
#endif
        }

        /// <summary>
        /// Handle exception thrown when receiving from remote endpoint.
        /// </summary>
        /// <param name="ex">The exception thrown.</param>
        /// <returns>Whether error should be ignored.</returns>
        protected virtual bool HandleException(Exception ex) => false;

        /// <summary>
        /// Create the upper-level connection and start pumping packets from the socket to the upper-level connection.
        /// </summary>
        public void Start()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(KcpSocketTransport));
            }
            if (_connection is not null)
            {
                throw new InvalidOperationException();
            }

            _connection = Activate();
            if (_connection is null)
            {
                throw new InvalidOperationException();
            }
            _cts = new CancellationTokenSource();
            RunReceiveLoop();
        }

        public void SetHandshakeHandler(int size, Func<UdpReceiveResult, ValueTask> handshakeHandler)
        {
            _handshakeSize = size;
            _handshakeHandler = handshakeHandler;
        }


#if NEED_SOCKET_SHIM
        /// <inheritdoc />
        public async ValueTask SendPacketAsync(Memory<byte> packet, CancellationToken cancellationToken = default)
        {
            if (_disposed)
            {
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();

            if (packet.Length > _mtu)
            {
                return;
            }

            byte[]? rentedArray = null;
            if (!MemoryMarshal.TryGetArray(packet, out ArraySegment<byte> segment))
            {
                rentedArray = ArrayPool<byte>.Shared.Rent(packet.Length);
                segment = new ArraySegment<byte>(rentedArray, 0, packet.Length);
                packet.CopyTo(segment.AsMemory());
            }

            try
            {
                using var saea = new AwaitableSocketAsyncEventArgs();
                saea.SetBuffer(segment.Array, segment.Offset, segment.Count);
                saea.SocketFlags = SocketFlags.None;
                saea.RemoteEndPoint = _endPoint;
                if (_socket.SendToAsync(saea))
                {
                    await saea.WaitAsync().ConfigureAwait(false);
                    saea.Reset();
                }

                if (saea.SocketError != SocketError.Success)
                {
                    throw new SocketException((int)saea.SocketError);
                }
            }
            finally
            {
                if (rentedArray is not null)
                {
                    ArrayPool<byte>.Shared.Return(rentedArray);
                }
            }
        }

        private static async ValueTask<int> ReceiveFromAsync(Socket socket, Memory<byte> buffer, EndPoint endPoint, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            byte[]? rentedArray = null;
            if (!MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> segment))
            {
                rentedArray = ArrayPool<byte>.Shared.Rent(buffer.Length);
                segment = new ArraySegment<byte>(rentedArray, 0, buffer.Length);
            }

            try
            {
                using var saea = new AwaitableSocketAsyncEventArgs();
                saea.SetBuffer(segment.Array, segment.Offset, segment.Count);
                saea.SocketFlags = SocketFlags.None;
                saea.RemoteEndPoint = endPoint;
                if (socket.SendToAsync(saea))
                {
                    await saea.WaitAsync().ConfigureAwait(false);
                    saea.Reset();
                }

                if (saea.SocketError != SocketError.Success)
                {
                    throw new SocketException((int)saea.SocketError);
                }

                if (rentedArray is not null)
                {
                    segment.AsMemory().CopyTo(buffer);
                }

                return saea.BytesTransferred;
            }
            finally
            {
                if (rentedArray is not null)
                {
                    ArrayPool<byte>.Shared.Return(rentedArray);
                }
            }
        }
#else
        /// <inheritdoc />
        public ValueTask SendPacketAsync(Memory<byte> packet, IPEndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (_disposed)
            {
                return default;
            }
            if (packet.Length > _mtu)
            {
                return default;
            }

            return new ValueTask(_udp.SendAsync(packet.ToArray(), endpoint, cancellationToken).AsTask());
        }
#endif


        private async void RunReceiveLoop()
        {
            CancellationToken cancellationToken = _cts?.Token ?? new CancellationToken(true);
            IKcpConversation? connection = _connection;
            if (connection is null || cancellationToken.IsCancellationRequested)
            {
                return;
            }

            using IMemoryOwner<byte> memoryOwner = AllocateBuffer(_mtu);
            try
            {
                Memory<byte> memory = memoryOwner.Memory;
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bytesReceived;
                    UdpReceiveResult result = default;
                    try
                    {
                        result = await _udp.ReceiveAsync(cancellationToken);
                        bytesReceived = result.Buffer.Length;
                    }
                    catch (Exception)
                    {
                        bytesReceived = 0;
                    }

                    if (bytesReceived != 0 && bytesReceived <= _mtu)
                    {
                        if (bytesReceived == _handshakeSize && _handshakeHandler != null)
                            await _handshakeHandler(result);
                        else
                            await connection.InputPakcetAsync(result.Buffer, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Do nothing
            }
            catch (Exception ex)
            {
                HandleExceptionWrapper(ex);
            }
        }

        private bool HandleExceptionWrapper(Exception ex)
        {
            bool result;
            try
            {
                result = HandleException(ex);
            }
            catch
            {
                result = false;
            }

            _connection?.SetTransportClosed();
            CancellationTokenSource? cts = Interlocked.Exchange(ref _cts, null);
            if (cts is not null)
            {
                cts.Cancel();
                cts.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Dispose all the managed and the unmanaged resources used by this instance.
        /// </summary>
        /// <param name="disposing">If managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    CancellationTokenSource? cts = Interlocked.Exchange(ref _cts, null);
                    if (cts is not null)
                    {
                        cts.Cancel();
                        cts.Dispose();
                    }
                    _connection?.Dispose();
                }

                _connection = null;
                _cts = null;
                _disposed = true;
            }
        }

        /// <summary>
        /// Dispose the unmanaged resources used by this instance.
        /// </summary>
        ~KcpSocketTransport()
        {
            Dispose(disposing: false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
