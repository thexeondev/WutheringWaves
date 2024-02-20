using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class InventoryController : Controller
{
    public InventoryController(PlayerSession session) : base(session)
    {
        // InventoryController.
    }

    [NetEvent(MessageId.NormalItemRequest)]
    public RpcResult OnNormalItemRequest() => Response(MessageId.NormalItemResponse, new NormalItemResponse());

    [NetEvent(MessageId.WeaponItemRequest)]
    public RpcResult OnWeaponItemRequest() => Response(MessageId.WeaponItemResponse, new WeaponItemResponse());

    [NetEvent(MessageId.PhantomItemRequest)]
    public RpcResult OnPhantomItemRequest() => Response(MessageId.PhantomItemResponse, new PhantomItemResponse());

    [NetEvent(MessageId.ItemExchangeInfoRequest)]
    public RpcResult OnItemExchangeInfoRequest() => Response(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());
}
