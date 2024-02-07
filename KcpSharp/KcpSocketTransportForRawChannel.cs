using System;
using System.Net;
using System.Net.Sockets;

namespace KcpSharp
{
    internal sealed class KcpSocketTransportForRawChannel : KcpSocketTransport<KcpRawChannel>, IKcpTransport<KcpRawChannel>
    {
        private readonly int? _conversationId;
        private readonly IPEndPoint _remoteEndPoint;
        private readonly KcpRawChannelOptions? _options;

        private Func<Exception, IKcpTransport<KcpRawChannel>, object?, bool>? _exceptionHandler;
        private object? _exceptionHandlerState;


        internal KcpSocketTransportForRawChannel(UdpClient socket, IPEndPoint endPoint, int? conversationId, KcpRawChannelOptions? options)
            : base(socket, options?.Mtu ?? KcpConversationOptions.MtuDefaultValue)
        {
            _remoteEndPoint = endPoint;
            _conversationId = conversationId;
            _options = options;
        }

        protected override KcpRawChannel Activate() => _conversationId.HasValue ? new KcpRawChannel(_remoteEndPoint, this, _conversationId.GetValueOrDefault(), _options) : new KcpRawChannel(_remoteEndPoint, this, _options);

        protected override bool HandleException(Exception ex)
        {
            if (_exceptionHandler is not null)
            {
                return _exceptionHandler.Invoke(ex, this, _exceptionHandlerState);
            }
            return false;
        }

        public void SetExceptionHandler(Func<Exception, IKcpTransport<KcpRawChannel>, object?, bool> handler, object? state)
        {
            _exceptionHandler = handler;
            _exceptionHandlerState = state;
        }
    }
}
