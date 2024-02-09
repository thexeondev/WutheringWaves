using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class GachaMessageHandler : MessageHandlerBase
{
    public GachaMessageHandler(KcpSession session) : base(session)
    {
        // GachaMessageHandler.
    }

    [MessageHandler(MessageId.GachaInfoRequest)]
    public async Task OnGachaInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.GachaInfoResponse, new GachaInfoResponse());
    }
}
