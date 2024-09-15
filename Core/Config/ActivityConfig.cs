using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("activity/activity.json")]
public class ActivityConfig : IConfig
{
    public ConfigType Type => ConfigType.Activity;
    public int Identifier => Id;

    public int Id { get; set; }
    public int ActivityType { get; set; }
    public List<int> PreShowGuideQuest { get; set; } = [];
    public int PreConditionGroupId { get; set; }
    public int PreviewDrop { get; set; }

}


