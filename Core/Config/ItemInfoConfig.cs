using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("item/iteminfo.json")]
public class ItemInfoConfig : IConfig
{
    public ConfigType Type => ConfigType.ItemInfo;
    public int Identifier => Id;

    public int Id { get; set; }
    public int ItemType { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<int> ShowTypes { get; set; } = [];
    public string AttributesDescription { get; set; } = string.Empty;
    public string BgDescription { get; set; } = string.Empty;
    public string Mesh { get; set; } = string.Empty;
    public int QualityId { get; set; }
    public int MainTypeId { get; set; }
    public int RedDotDisableRule { get; set; }
    public int UseCountLimit { get; set; }
    public int SortIndex { get; set; }
    public int MaxCapcity { get; set; }
    public int MaxStackableNum { get; set; }
    public int UseLevel { get; set; }
    public int BeginTimeStamp { get; set; }
    public int DurationStamp { get; set; }
    public bool ShowUseButton { get; set; }
    public int ObtainedShow { get; set; }
    public string ObtainedShowDescription { get; set; } = string.Empty;
    public int EntityConfig { get; set; }
    public int NumLimit { get; set; }
    public bool ShowInBag { get; set; }
    public bool Destructible { get; set; }
    public int ItemBuffType { get; set; }
    public bool SpecialItem { get; set; }
    public bool UiPlayItem { get; set; }
}
