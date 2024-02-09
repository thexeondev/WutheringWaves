using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class WorldMapController : Controller
{
    public WorldMapController(PlayerSession session) : base(session)
    {
        // WorldMapMessageHandler.
    }

    [NetEvent(MessageId.MapTraceInfoRequest)]
    public ResponseMessage OnMapTraceInfoRequest() => Response(MessageId.MapTraceInfoResponse, new MapTraceInfoResponse());
}
