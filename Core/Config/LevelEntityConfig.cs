using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("level_entity/levelentityconfig.json")]
public class LevelEntityConfig : IConfig
{
    public ConfigType Type => ConfigType.LevelEntity;
    public int Identifier => Id;

    public int Id { get; set; }
    public int MapId { get; set; }
    public int EntityId { get; set; }
    public string BlueprintType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool InSleep { get; set; }
    public bool IsHidden { get; set; }
    public int AreaId { get; set; }
    public Transform[] Transform { get; set; } = [];
}

public class Transform
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
}
