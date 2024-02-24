using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
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
    public RpcResult OnNormalItemRequest(ModelManager modelManager) => Response(MessageId.NormalItemResponse, new NormalItemResponse
    {
        NormalItemList = { modelManager.Inventory.ItemList }
    });

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
    public RpcResult OnEquipTakeOnRequest(EquipTakeOnRequest request, ModelManager modelManager, CreatureController creatureController, RoleController roleController, ConfigManager configManager)
    {
        WeaponItem? weapon = modelManager.Inventory.GetWeaponById(request.Data.EquipIncId);
        if (weapon == null) return Response(MessageId.EquipTakeOnResponse, new EquipTakeOnResponse
        {
            ErrorCode = (int)ErrorCode.ErrItemIdInvaild
        });

        WeaponConfig weaponConf = configManager.GetConfig<WeaponConfig>(weapon.Id)!;

        roleInfo? role = modelManager.Roles.GetRoleById(request.Data.RoleId);
        if (role == null) return Response(MessageId.EquipTakeOnResponse, new EquipTakeOnResponse
        {
            ErrorCode = (int)ErrorCode.NotValidRole
        });

        // Take off previous weapon
        WeaponItem? prevWeapon = modelManager.Inventory.WeaponList.SingleOrDefault(weapon => weapon.RoleId == role.RoleId);
        if (prevWeapon != null) prevWeapon.RoleId = 0;

        // Set new weapon
        weapon.RoleId = role.RoleId;
        roleController.ApplyWeaponPropertiesToRole(role.RoleId, weaponConf);

        // Update entity (if this role is currently active)
        PlayerEntity? entity = creatureController.GetPlayerEntityByRoleId(request.Data.RoleId);
        entity?.ChangeEquipment(weapon.Id);
        entity?.ChangeGameplayAttributes(role.GetAttributeList());

        // Response
        EquipTakeOnResponse response = new()
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
        };

        if (prevWeapon != null)
        {
            response.DataList.Add(new RoleLoadEquipData
            {
                EquipIncId = prevWeapon.IncrId
            });
        }

        return Response(MessageId.EquipTakeOnResponse, response);
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        await Session.Push(MessageId.ItemPkgOpenNotify, new ItemPkgOpenNotify
        {
            OpenPkg = { 0, 2, 1, 3, 4, 5, 6, 7 }
        });
    }

    [GameEvent(GameEventType.DebugUnlockAllItems)]
    public void DebugUnlockAllWeapons(ConfigManager configManager, ModelManager modelManager)
    {
        foreach (WeaponConfig weaponConf in configManager.Enumerate<WeaponConfig>())
        {
            modelManager.Inventory.AddNewWeapon(weaponConf.ItemId);
        }

        foreach (ItemInfoConfig itemInfo in configManager.Enumerate<ItemInfoConfig>())
        {
            modelManager.Inventory.AddItem(itemInfo.Id, itemInfo.MaxStackableNum);
        }
    }
}
