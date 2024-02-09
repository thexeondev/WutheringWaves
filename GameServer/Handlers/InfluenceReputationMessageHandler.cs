using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class InfluenceReputationMessageHandler : MessageHandlerBase
{
    public InfluenceReputationMessageHandler(PlayerSession session) : base(session)
    {
        // InfluenceReputationMessageHandler.
    }

    [MessageHandler(MessageId.InfluenceInfoRequest)]
    public async Task OnInfluenceInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.InfluenceInfoResponse, new InfluenceInfoResponse());
    }
}
