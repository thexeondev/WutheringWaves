using Core.Config;
using Protocol;

namespace GameServer.Extensions.Logic;
internal static class RoleInfoExtensions
{
    public static IEnumerable<GameplayAttributeData> GetAttributeList(this roleInfo role)
    {
        return role.BaseProp.Select(prop => new GameplayAttributeData
        {
            AttributeType = prop.Key,
            BaseValue = prop.Value,
            CurrentValue = prop.Value + ((role.AddProp.SingleOrDefault(p => p.Key == prop.Key)?.Value) ?? 0),
        });
    }

    public static void ApplyWeaponProperties(this roleInfo role, WeaponConfig weaponConf)
    {
        role.AddProp.Clear();

        if (weaponConf.FirstPropId != null)
        {
            role.AddProp.Add(new ArrayIntInt
            {
                Key = weaponConf.FirstPropId.Id,
                Value = (int)weaponConf.FirstPropId.Value
            });
        }

        if (weaponConf.SecondPropId != null)
        {
            role.AddProp.Add(new ArrayIntInt
            {
                Key = weaponConf.SecondPropId.Id,
                Value = (int)weaponConf.SecondPropId.Value
            });
        }
    }
}
