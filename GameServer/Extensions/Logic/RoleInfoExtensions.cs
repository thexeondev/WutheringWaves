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

    public static void ApplyLvGrowthProperties(this roleInfo role, ConfigManager configManager )
    {
        int level = role.Level;
        int breach = role.Breakthrough;
        float LifemaxRatio = 1.0f;
        float AtkRatio = 1.0f;
        float DefRatio = 1.0f;


        List<ArrayIntInt> baselist = [.. role.BaseProp];
        if (baselist.Count > 0)
        {
            foreach (RolePropertyGrowthConfig GrowthConfig in configManager.Enumerate<RolePropertyGrowthConfig>())
            {
                if (GrowthConfig.Level == level && GrowthConfig.BreachLevel == breach)
                {
                    LifemaxRatio = GrowthConfig.LifeMaxRatio / 10000;
                    AtkRatio = GrowthConfig.AtkRatio / 10000;
                    DefRatio = GrowthConfig.DefRatio / 10000;
                }
            }
            ArrayIntInt lv = baselist[(int)EAttributeType.Lv - 1];
            lv.Value = level;
            ArrayIntInt lifemax = baselist[(int)EAttributeType.LifeMax - 1];
            lifemax.Value = (int)(lifemax.Value * LifemaxRatio);
            ArrayIntInt life = baselist[(int)EAttributeType.Life - 1];
            life.Value = lifemax.Value;
            ArrayIntInt atk = baselist[(int)EAttributeType.Atk - 1];
            atk.Value = (int)(atk.Value * AtkRatio);
            ArrayIntInt def = baselist[(int)EAttributeType.Def - 1];
            def.Value = (int)(def.Value * DefRatio);
        }

        role.BaseProp.Clear();
        role.BaseProp.Add(baselist);
    }
}
