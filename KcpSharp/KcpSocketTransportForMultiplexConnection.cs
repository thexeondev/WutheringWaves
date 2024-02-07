using System;
using System.Net;
using System.Net.Sockets;

namespace KcpSharp
{
    internal sealed class KcpSocketTransportForMultiplexConnection<T> : KcpSocketTransport<KcpMultiplexConnection<T>>, IKcpTransport<IKcpMultiplexConnection<T>>
    {
        private readonly Action<T?>? _disposeAction;
        private Func<Exception, IKcpTransport<IKcpMultiplexConnection<T>>, object?, bool>? _exceptionHandler;
        private object? _exceptionHandlerState;

        internal KcpSocketTransportForMultiplexConnection(UdpClient socket, int mtu)
            : base(socket, mtu)
        { }

        internal KcpSocketTransportForMultiplexConnection(UdpClient socket, int mtu, Action<T?>? disposeAction)
            : base(socket, mtu)
        {
            _disposeAction = disposeAction;
        }

        protected override KcpMultiplexConnection<T> Activate() => new KcpMultiplexConnection<T>(this, _disposeAction);

        IKcpMultiplexConnection<T> IKcpTransport<IKcpMultiplexConnection<T>>.Connection => Connection;

        protected override bool HandleException(Exception ex)
        {
            if (_exceptionHandler is not null)
            {
                return _exceptionHandler.Invoke(ex, this, _exceptionHandlerState);
            }
            return false;
        }

        public void SetExceptionHandler(Func<Exception, IKcpTransport<IKcpMultiplexConnection<T>>, object?, bool> handler, object? state)
        {
            _exceptionHandler = handler;
            _exceptionHandlerState = state;
        }
    }
}
