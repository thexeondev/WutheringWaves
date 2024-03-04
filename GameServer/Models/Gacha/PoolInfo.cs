using GameServer.Network;

namespace GameServer.Controllers.Gacha;
internal class PoolInfo : Controller
{
    public readonly GachaPool BaseRole = new(1, "BaseRole", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool BaseWeapon1 = new(31, "BaseWeapon1", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool BaseWeapon2 = new(32, "BaseWeapon2", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool BaseWeapon3 = new(33, "BaseWeapon3", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool BaseWeapon4 = new(34, "BaseWeapon4", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool BaseWeapon5 = new(35, "BaseWeapon5", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool RoleUp1 = new(100001, "RoleUp1", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool RoleUp2 = new(100002, "RoleUp2", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool WeaponUp1 = new(200001, "WeaponUp1", 60, 10, 80, [93.2f, 6f, 0.8f]);
    public readonly GachaPool WeaponUp2 = new(200002, "WeaponUp2", 60, 10, 80, [93.2f, 6f, 0.8f]);

    public PoolInfo(PlayerSession session) : base(session)
    {
        BaseRole.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseRole.AddItemsFour([1303, 1602, 1102, 1204, 1403, 1103, 1402, 1202, 1601]);
        BaseRole.AddItemsFive([1302, 1203, 1405, 1104, 1301, 1404, 1503]);

        BaseWeapon1.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseWeapon1.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        BaseWeapon1.AddItemsFive([21010015]);

        BaseWeapon2.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseWeapon2.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        BaseWeapon2.AddItemsFive([21020015]);

        BaseWeapon3.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseWeapon3.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        BaseWeapon3.AddItemsFive([21030015]);

        BaseWeapon4.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseWeapon4.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        BaseWeapon4.AddItemsFive([21040015]);

        BaseWeapon5.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        BaseWeapon5.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        BaseWeapon5.AddItemsFive([21050015]);

        RoleUp1.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        RoleUp1.AddItemsFour([1303, 1602, 1102, 1204, 1403, 1103, 1402, 1202, 1601, 21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        RoleUp1.AddItemsFive([1404]);

        RoleUp2.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        RoleUp2.AddItemsFour([1303, 1602, 1102, 1204, 1403, 1103, 1402, 1202, 1601, 21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        RoleUp2.AddItemsFive([1302]);

        WeaponUp1.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        WeaponUp1.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        WeaponUp1.AddItemsFive([21010016]);

        WeaponUp2.AddItemsThree([21010013, 21020013, 21030013, 21040013, 21050013, 21010023, 21020023, 21030023, 21040023, 21050023, 21010043, 21020043, 21030043, 21040043, 21050043]);
        WeaponUp2.AddItemsFour([21010024, 21020024, 21030024, 21040024, 21050024, 21010044, 21020044, 21030044, 21040044, 21050044, 21010064, 21020064, 21030064, 21040064, 21050064]);
        WeaponUp2.AddItemsFive([21050016]);
    }
}