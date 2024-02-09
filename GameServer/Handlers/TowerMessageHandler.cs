using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class TowerMessageHandler : MessageHandlerBase
{
    public TowerMessageHandler(PlayerSession session) : base(session)
    {
        // TowerMessageHandler.
    }

    [MessageHandler(MessageId.TowerChallengeRequest)]
    public async Task OnTowerChallengeRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.TowerChallengeResponse, new TowerChallengeResponse());
    }

    [MessageHandler(MessageId.CycleTowerChallengeRequest)]
    public async Task OnCycleTowerChallengeRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.CycleTowerChallengeResponse, new CycleTowerChallengeResponse());
    }
}
