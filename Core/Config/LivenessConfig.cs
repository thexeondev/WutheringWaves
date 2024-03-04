using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("daily_activity/liveness.json")]
public class LivenessConfig : IConfig
{
    public ConfigType Type => ConfigType.Liveness;
    public int Identifier => Id;

    public int Id { get; set; }

    public int Goal { get; set; }

    public int DropId { get; set; }

}

[ConfigCollection("daily_activity/livenesstask.json")]
public class LivenesstaskConfig : IConfig
{
    public ConfigType Type => ConfigType.Livenesstask;
    public int Identifier => TaskId;

    public int TaskId { get; set; }
    public int UnlockCondition { get; set; }

}



