using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class ExchangeRewardMessageHandler : MessageHandlerBase
{
    public ExchangeRewardMessageHandler(KcpSession session) : base(session)
    {
        // ExchangeRewardMessageHandler.
    }

    [MessageHandler(MessageId.ExchangeRewardInfoRequest)]
    public async Task OnExchangeRewardInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ExchangeRewardInfoResponse, new ExchangeRewardInfoResponse());
    }
}
