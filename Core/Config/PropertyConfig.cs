using Core.Config.Attributes;

namespace Core.Config;


[ConfigCollection("property/rolepropertygrowth.json")]
public class RolePropertyGrowthConfig : IConfig
{
    public ConfigType Type => ConfigType.rolepropertygrowth;

    public int Identifier => Id;
    public int Id { get; set; }
    public int Level { get; set; }
    public int BreachLevel { get; set; }
    public int LifeMaxRatio { get; set; }
    public int AtkRatio { get; set; }
    public int DefRatio { get; set; }

}

[ConfigCollection("property/monsterpropertygrowth.json")]
public class MonsterPropertyGrowthConfig : IConfig
{
    public ConfigType Type => ConfigType.monsterpropertygrowth;

    public int Identifier => Id;
    public int Id { get; set; }
    public int Level { get; set; }
    public int LifeMaxRatio { get; set; }
    public int AtkRatio { get; set; }
    public int DefRatio { get; set; }
    public int HardnessMaxRatio { get; set; }
    public int HardnessRatio { get; set; }
    public int HardnessRecoverRatio { get; set; }
    public int RageMaxRatio { get; set; }
    public int RageRatio { get; set; }
    public int RageRecoverRatio { get; set; }
}

//[ConfigCollection("property/weaponpropertygrowth")]
//public class WraponPropertyGrowthConfig : IConfig
//{
//    public ConfigType Type => ConfigType.rolepropertygrowth;

//    public int Identifier => Id;
//    public int Id { get; set; }
//    public int CurveId { get; set; }
//    public int BreachLevel { get; set; }
//    public int CurveValue { get; set; }

//}