using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class RoguelikeMessageHandler : MessageHandlerBase
{
    public RoguelikeMessageHandler(PlayerSession session) : base(session)
    {
        // RoguelikeMessageHandler.
    }

    [MessageHandler(MessageId.RoguelikeSeasonDataRequest)]
    public async Task OnRoguelikeSeasonDataRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.RoguelikeSeasonDataResponse, new RoguelikeSeasonDataResponse());
    }
}
