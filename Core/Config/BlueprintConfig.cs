using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("blueprint/blueprintconfig.json")]
public class BlueprintConfig : IConfig
{
    public ConfigType Type => ConfigType.Blueprint;
    public int Identifier => Id;

    public int Id { get; set; }
    public string BlueprintType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string EntityLogic { get; set; } = string.Empty;
    public int ModelId { get; set; }
    public int HalfHeight { get; set; }
}
