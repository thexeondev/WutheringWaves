using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("achievement/achievement.json")]
public class AchievementConfig : IConfig
{
    public ConfigType Type => ConfigType.Achievement;
    public int Identifier => Id;

    public int Id { get; set; }
    public int GroupId { get; set; }
    public int Level  { get; set; }
}

[ConfigCollection("achievement/achievementgroup.json")]
public class AchievementGroupConfig : IConfig
{
    public ConfigType Type => ConfigType.AchievementGroup;
    public int Identifier => Id;

    public int Id { get; set; }
    public int Category { get; set; }
    public int Sort { get; set; }

}



