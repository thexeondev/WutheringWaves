using Core.Config.Attributes;
using Core.Config.Types;

namespace Core.Config;

[ConfigCollection("weapon/weaponconf.json")]
public class WeaponConfig : IConfig
{
    public ConfigType Type => ConfigType.Weapon;

    public int Identifier => ItemId;

    public int ItemId { get; set; }
    public string WeaponName { get; set; } = string.Empty;
    public int QualityId { get; set; }
    public int WeaponType { get; set; }
    public int ModelId { get; set; }
    public int TransformId { get; set; }
    public List<int> Models { get; set; } = [];
    public int ResonLevelLimit { get; set; }
    public PropConfig? FirstPropId { get; set; }
    public int FirstCurve { get; set; }
    public PropConfig? SecondPropId { get; set; }
    public int SecondCurve { get; set; }
    public int ResonId { get; set; }
    public int LevelId { get; set; }
    public int BreachId { get; set; }
    public string Desc { get; set; } = string.Empty;
    public string TypeDescription { get; set; } = string.Empty;
    public string AttributesDescription { get; set; } = string.Empty;
    public string BgDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string IconMiddle { get; set; } = string.Empty;
    public string IconSmall { get; set; } = string.Empty;
    public string Mesh { get; set; } = string.Empty;
    public int MaxCapcity { get; set; }
    public List<object> ItemAccess { get; set; } = [];
    public int ObtainedShow { get; set; }
    public string ObtainedShowDescription { get; set; } = string.Empty;
    public int NumLimit { get; set; }
    public bool ShowInBag { get; set; }
    public int SortIndex { get; set; }
    public string ResonanceIcon { get; set; } = string.Empty;
    public int HiddenTime { get; set; }
    public bool Destructible { get; set; }
    public int RedDotDisableRule { get; set; }
}
