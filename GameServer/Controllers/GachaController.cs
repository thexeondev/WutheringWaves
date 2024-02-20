using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class GachaController : Controller
{
    public GachaController(PlayerSession session) : base(session)
    {
        // GachaController.
    }

    [NetEvent(MessageId.GachaInfoRequest)]
    public RpcResult OnGachaInfoRequest() => Response(MessageId.GachaInfoResponse, new GachaInfoResponse());
}
