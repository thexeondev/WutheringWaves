using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class ShopController : Controller
{
    public ShopController(PlayerSession session) : base(session)
    {
        // ShopController.
    }

    [NetEvent(MessageId.PayShopInfoRequest)]
    public RpcResult OnPayShopInfoRequest() => Response(MessageId.PayShopInfoResponse, new PayShopInfoResponse());
}
