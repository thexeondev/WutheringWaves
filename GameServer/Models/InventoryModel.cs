using Protocol;

namespace GameServer.Models;
internal class InventoryModel
{
    private int _itemIncrId;

    public List<NormalItem> ItemList { get; } = [];
    public List<WeaponItem> WeaponList { get; } = [];

    public WeaponItem? GetEquippedWeapon(int roleId) => WeaponList.SingleOrDefault(weapon => weapon.RoleId == roleId);

    public WeaponItem? GetWeaponById(int incrId) => WeaponList.SingleOrDefault(weapon => weapon.IncrId == incrId);

    public int GetItemCount(int itemId) => ItemList.SingleOrDefault(item => item.Id == itemId)?.Count ?? 0;

    public bool TryUseItem(int itemId, int amount)
    {
        int currentAmount = GetItemCount(itemId);
        if (amount > currentAmount) return false;

        AddItem(itemId, -amount);
        return true;
    }

    public void AddItem(int itemId, int amount)
    {
        NormalItem? item = ItemList.SingleOrDefault(item => item.Id == itemId);
        if (item != null)
        {
            item.Count += amount;
            return;
        }

        ItemList.Add(new NormalItem
        {
            Id = itemId,
            Count = amount
        });
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
