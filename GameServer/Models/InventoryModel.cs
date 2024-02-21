using Core.Config;
using Protocol;

namespace GameServer.Models;
internal class InventoryModel
{
    private int _itemIncrId;

    public List<WeaponItem> WeaponList { get; } = [];

    public WeaponItem? GetWeaponById(int incrId)
    {
        return WeaponList.SingleOrDefault(weapon => weapon.IncrId == incrId);
    }

    public WeaponItem AddNewWeapon(int weaponId)
    {
        WeaponItem weapon = new()
        {
            Id = weaponId,
            IncrId = ++_itemIncrId,
            WeaponLevel = 1,
            WeaponResonLevel = 1
        };

        WeaponList.Add(weapon);
        return weapon;
    }
}
