using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class WorldMapMessageHandler : MessageHandlerBase
{
    public WorldMapMessageHandler(PlayerSession session) : base(session)
    {
        // WorldMapMessageHandler.
    }

    [MessageHandler(MessageId.MapTraceInfoRequest)]
    public async Task OnMapTraceInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.MapTraceInfoResponse, new MapTraceInfoResponse());
    }
}
