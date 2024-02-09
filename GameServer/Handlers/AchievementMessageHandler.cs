using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class AchievementMessageHandler : MessageHandlerBase
{
    public AchievementMessageHandler(PlayerSession session) : base(session)
    {
        // AchievementMessageHandler.
    }

    [MessageHandler(MessageId.AchievementInfoRequest)]
    public async Task OnAchievementInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.AchievementInfoResponse, new AchievementInfoResponse());
    }
}
