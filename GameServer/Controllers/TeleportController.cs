using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class TeleportController : Controller
{
    public TeleportController(PlayerSession session) : base(session)
    {
        // TeleportController.
    }

    [NetEvent(MessageId.TeleportFinishRequest)]
    public RpcResult OnTeleportFinishRequest() => Response(MessageId.TeleportFinishResponse, new TeleportFinishResponse());

    [NetEvent(MessageId.TeleportDataRequest)]
    public RpcResult OnTeleportDataRequest() => Response(MessageId.TeleportDataResponse, new TeleportDataResponse());
}
