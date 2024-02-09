using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class InventoryController : Controller
{
    public InventoryController(PlayerSession session) : base(session)
    {
        // InventoryMessageHandler.
    }

    [NetEvent(MessageId.NormalItemRequest)]
    public ResponseMessage OnNormalItemRequest() => Response(MessageId.NormalItemResponse, new NormalItemResponse());

    [NetEvent(MessageId.WeaponItemRequest)]
    public ResponseMessage OnWeaponItemRequest() => Response(MessageId.WeaponItemResponse, new WeaponItemResponse());

    [NetEvent(MessageId.PhantomItemRequest)]
    public ResponseMessage OnPhantomItemRequest() => Response(MessageId.PhantomItemResponse, new PhantomItemResponse());

    [NetEvent(MessageId.ItemExchangeInfoRequest)]
    public ResponseMessage OnItemExchangeInfoRequest() => Response(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());
}
