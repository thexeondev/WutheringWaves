using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using GameServer.Systems.Event;
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
    public RpcResult OnWeaponItemRequest(ModelManager modelManager) => Response(MessageId.WeaponItemResponse, new WeaponItemResponse
    {
        WeaponItemList =
        {
            modelManager.Inventory.WeaponList
        }
    });

    [NetEvent(MessageId.PhantomItemRequest)]
    public RpcResult OnPhantomItemRequest() => Response(MessageId.PhantomItemResponse, new PhantomItemResponse());

    [NetEvent(MessageId.ItemExchangeInfoRequest)]
    public RpcResult OnItemExchangeInfoRequest() => Response(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());

    [NetEvent(MessageId.EquipTakeOnRequest)]
    public async Task<RpcResult> OnEquipTakeOnRequest(EquipTakeOnRequest request, ModelManager modelManager, CreatureController creatureController)
    {
        WeaponItem? weapon = modelManager.Inventory.GetWeaponById(request.Data.EquipIncId);
        if (weapon == null) return Response(MessageId.EquipTakeOnResponse, new EquipTakeOnResponse
        {
            ErrorCode = (int)ErrorCode.ErrItemIdInvaild
        });

        PlayerEntity? entity = creatureController.GetPlayerEntityByRoleId(request.Data.RoleId);
        if (entity != null) 
        {
            EntityEquipComponent equipComponent = entity.ComponentSystem.Get<EntityEquipComponent>();
            equipComponent.WeaponId = weapon.Id;

            await Session.Push(MessageId.EntityEquipChangeNotify, new EntityEquipChangeNotify
            {
                EntityId = entity.Id,
                EquipComponent = equipComponent.Pb.EquipComponent
            });
        }

        return Response(MessageId.EquipTakeOnResponse, new EquipTakeOnResponse
        {
            DataList =
            {
                new RoleLoadEquipData
                {
                    RoleId = request.Data.RoleId,
                    Pos = request.Data.Pos,
                    EquipIncId = request.Data.EquipIncId
                }
            }
        });
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        await Session.Push(MessageId.ItemPkgOpenNotify, new ItemPkgOpenNotify
        {
            OpenPkg = { 0, 2, 1, 3, 4, 5, 6, 7 }
        });
    }

    [GameEvent(GameEventType.DebugUnlockAllWeapons)]
    public void DebugUnlockAllWeapons(ConfigManager configManager, ModelManager modelManager)
    {
        foreach (WeaponConfig weaponConf in configManager.Enumerate<WeaponConfig>())
        {
            modelManager.Inventory.AddNewWeapon(weaponConf.ItemId);
        }
    }
}
