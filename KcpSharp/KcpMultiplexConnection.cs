using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace KcpSharp
{
    /// <summary>
    /// Multiplex many channels or conversations over the same transport.
    /// </summary>
    /// <typeparam name="T">The state of the channel.</typeparam>
    public sealed class KcpMultiplexConnection<T> : IKcpTransport, IKcpConversation, IKcpMultiplexConnection<T>
    {
        private readonly IKcpTransport _transport;

        private readonly ConcurrentDictionary<int, (IKcpConversation Conversation, T? State)> _conversations = new();
        private bool _transportClosed;
        private bool _disposed;

        private readonly Action<T?>? _disposeAction;

        /// <summary>
        /// Construct a multiplexed connection over a transport.
        /// </summary>
        /// <param name="transport">The underlying transport.</param>
        public KcpMultiplexConnection(IKcpTransport transport)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
            _disposeAction = null;
        }

        /// <summary>
        /// Construct a multiplexed connection over a transport.
        /// </summary>
        /// <param name="transport">The underlying transport.</param>
        /// <param name="disposeAction">The action to invoke when state object is removed.</param>
        public KcpMultiplexConnection(IKcpTransport transport, Action<T?>? disposeAction)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
            _disposeAction = disposeAction;
        }

        private void CheckDispose()
        {
            if (_disposed)
            {
                ThrowObjectDisposedException();
            }
        }

        private static void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException(nameof(KcpMultiplexConnection<T>));
        }

        /// <summary>
        /// Process a newly received packet from the transport.
        /// </summary>
        /// <param name="packet">The content of the packet with conversation ID.</param>
        /// <param name="cancellationToken">A token to cancel this operation.</param>
        /// <returns>A <see cref="ValueTask"/> that completes when the packet is handled by the corresponding channel or conversation.</returns>
        public ValueTask InputPakcetAsync(ReadOnlyMemory<byte> packet, CancellationToken cancellationToken = default)
        {
            ReadOnlySpan<byte> span = packet.Span;
            if (span.Length < 4)
            {
                return default;
            }
            if (_transportClosed || _disposed)
            {
                return default;
            }
            int id = (int)BinaryPrimitives.ReadUInt32LittleEndian(span);
            if (_conversations.TryGetValue(id, out (IKcpConversation Conversation, T? State) value))
            {
                return value.Conversation.InputPakcetAsync(packet, cancellationToken);
            }
            return default;
        }

        /// <summary>
        /// Determine whether the multiplex connection contains a conversation with the specified id.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <returns>True if the multiplex connection contains the specified conversation. Otherwise false.</returns>
        public bool Contains(int id)
        {
            CheckDispose();
            return _conversations.ContainsKey(id);
        }

        /// <summary>
        /// Create a raw channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="options">The options of the <see cref="KcpRawChannel"/>.</param>
        /// <returns>The raw channel created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public KcpRawChannel CreateRawChannel(int id, IPEndPoint remoteEndPoint, KcpRawChannelOptions? options = null)
        {
            KcpRawChannel? channel = new KcpRawChannel(remoteEndPoint, this, id, options);
            try
            {
                RegisterConversation(channel, id, default);
                if (_transportClosed)
                {
                    channel.SetTransportClosed();
                }
                return Interlocked.Exchange<KcpRawChannel?>(ref channel, null)!;
            }
            finally
            {
                if (channel is not null)
                {
                    channel.Dispose();
                }
            }
        }

        /// <summary>
        /// Create a raw channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state of this channel.</param>
        /// <param name="options">The options of the <see cref="KcpRawChannel"/>.</param>
        /// <returns>The raw channel created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public KcpRawChannel CreateRawChannel(int id, IPEndPoint remoteEndPoint, T state, KcpRawChannelOptions? options = null)
        {
            var channel = new KcpRawChannel(remoteEndPoint, this, id, options);
            try
            {
                RegisterConversation(channel, id, state);
                if (_transportClosed)
                {
                    channel.SetTransportClosed();
                }
                return Interlocked.Exchange<KcpRawChannel?>(ref channel, null)!;
            }
            finally
            {
                if (channel is not null)
                {
                    channel.Dispose();
                }
            }
        }

        /// <summary>
        /// Create a conversation with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="options">The options of the <see cref="KcpConversation"/>.</param>
        /// <returns>The KCP conversation created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public KcpConversation CreateConversation(int id, IPEndPoint remoteEndPoint, KcpConversationOptions? options = null)
        {
            var conversation = new KcpConversation(remoteEndPoint, this, id, options);
            try
            {
                RegisterConversation(conversation, id, default);
                if (_transportClosed)
                {
                    conversation.SetTransportClosed();
                }
                return Interlocked.Exchange<KcpConversation?>(ref conversation, null)!;
            }
            finally
            {
                if (conversation is not null)
                {
                    conversation.Dispose();
                }
            }
        }

        /// <summary>
        /// Create a conversation with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state of this conversation.</param>
        /// <param name="options">The options of the <see cref="KcpConversation"/>.</param>
        /// <returns>The KCP conversation created.</returns>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public KcpConversation CreateConversation(int id, IPEndPoint remoteEndPoint, T state, KcpConversationOptions? options = null)
        {
            var conversation = new KcpConversation(remoteEndPoint, this, id, options);
            try
            {
                RegisterConversation(conversation, id, state);
                if (_transportClosed)
                {
                    conversation.SetTransportClosed();
                }
                return Interlocked.Exchange<KcpConversation?>(ref conversation, null)!;
            }
            finally
            {
                if (conversation is not null)
                {
                    conversation.Dispose();
                }
            }
        }

        /// <summary>
        /// Register a conversation or channel with the specified conversation ID and user state.
        /// </summary>
        /// <param name="conversation">The conversation or channel to register.</param>
        /// <param name="id">The conversation ID.</param>
        /// <exception cref="ArgumentNullException"><paramref name="conversation"/> is not provided.</exception>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public void RegisterConversation(IKcpConversation conversation, int id)
            => RegisterConversation(conversation, id, default);

        /// <summary>
        /// Register a conversation or channel with the specified conversation ID and user state.
        /// </summary>
        /// <param name="conversation">The conversation or channel to register.</param>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state</param>
        /// <exception cref="ArgumentNullException"><paramref name="conversation"/> is not provided.</exception>
        /// <exception cref="ObjectDisposedException">The current instance is disposed.</exception>
        /// <exception cref="InvalidOperationException">Another channel or conversation with the same ID was already registered.</exception>
        public void RegisterConversation(IKcpConversation conversation, int id, T? state)
        {
            if (conversation is null)
            {
                throw new ArgumentNullException(nameof(conversation));
            }

            CheckDispose();
            (IKcpConversation addedConversation, T? _) = _conversations.GetOrAdd(id, (conversation, state));
            if (!ReferenceEquals(addedConversation, conversation))
            {
                throw new InvalidOperationException("Duplicated conversation.");
            }
            if (_disposed)
            {
                if (_conversations.TryRemove(id, out (IKcpConversation Conversation, T? State) value) && _disposeAction is not null)
                {
                    _disposeAction.Invoke(value.State);
                }
                ThrowObjectDisposedException();
            }
        }

        /// <summary>
        /// Unregister a conversation or channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <returns>The conversation unregistered. Returns null when the conversation with the specified ID is not found.</returns>
        public IKcpConversation? UnregisterConversation(int id)
        {
            return UnregisterConversation(id, out _);
        }

        /// <summary>
        /// Unregister a conversation or channel with the specified conversation ID.
        /// </summary>
        /// <param name="id">The conversation ID.</param>
        /// <param name="state">The user state.</param>
        /// <returns>The conversation unregistered. Returns null when the conversation with the specified ID is not found.</returns>
        public IKcpConversation? UnregisterConversation(int id, out T? state)
        {
            if (!_transportClosed && !_disposed && _conversations.TryRemove(id, out (IKcpConversation Conversation, T? State) value))
            {
                value.Conversation.SetTransportClosed();
                state = value.State;
                if (_disposeAction is not null)
                {
                    _disposeAction.Invoke(state);
                }
                return value.Conversation;
            }
            state = default;
            return default;
        }

        /// <inheritdoc />
        public ValueTask SendPacketAsync(Memory<byte> packet, IPEndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (_transportClosed || _disposed)
            {
                return default;
            }
            return _transport.SendPacketAsync(packet, endpoint, cancellationToken);
        }

        /// <inheritdoc />
        public void SetTransportClosed()
        {
            _transportClosed = true;
            foreach ((IKcpConversation conversation, T? _) in _conversations.Values)
            {
                conversation.SetTransportClosed();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _transportClosed = true;
            _disposed = true;
            while (!_conversations.IsEmpty)
            {
                foreach (int id in _conversations.Keys)
                {
                    if (_conversations.TryRemove(id, out (IKcpConversation Conversation, T? State) value))
                    {
                        value.Conversation.Dispose();
                        if (_disposeAction is not null)
                        {
                            _disposeAction.Invoke(value.State);
                        }
                    }
                }
            }
        }

        public void SetHandshakeHandler(int size, Func<UdpReceiveResult, ValueTask> handshakeHandler)
        {
            throw new NotImplementedException("SetHandshakeHandler not designed for this type of transport.");
        }
    }
}
