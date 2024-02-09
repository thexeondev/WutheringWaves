using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class LordGymMessageHandler : MessageHandlerBase
{
    public LordGymMessageHandler(KcpSession session) : base(session)
    {
        // LordGymMessageHandler.
    }

    [MessageHandler(MessageId.LordGymInfoRequest)]
    public async Task OnLordGymInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.LordGymInfoResponse, new LordGymInfoResponse());
    }
}
