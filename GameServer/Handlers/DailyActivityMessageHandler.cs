using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class DailyActivityMessageHandler : MessageHandlerBase
{
    public DailyActivityMessageHandler(PlayerSession session) : base(session)
    {
        // DailyActivityMessageHandler.
    }

    [MessageHandler(MessageId.ActivityRequest)]
    public async Task OnActivityRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ActivityResponse, new ActivityResponse());
    }

    [MessageHandler(MessageId.LivenessRequest)]
    public async Task OnLivenessRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.LivenessResponse, new LivenessResponse());
    }
}
