using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("battle_pass/battlepass.json")]
public class BattlePassConfig : IConfig
{
    public ConfigType Type => ConfigType.BattlePass;
    public int Identifier => Id; 
    public int Id { get; set; }
    public int InitialLevel { get; set; }
    public int LevelLimit { get; set; }
    public int LevelUpExp { get; set; }
    public bool IsRecurringLevel { get; set; }
    //public List<int> FreeRecurringReward { get; set; } = [];
    //public List<int> PayRecurringReward { get; set; } = [];
    public int RecurringLevelExp { get; set; }
    public int WeekExpLimit { get; set; }
}

[ConfigCollection("battle_pass/battlepasstask.json")]
public class BattlePassTaskConfig : IConfig
{
    public ConfigType Type => ConfigType.BattlePassTask;
    public int Identifier => TaskId;

    public int TaskId { get; set; }
    //public required string TaskName { get; set; }
    public int UpdateType { get; set; }
    
}

[ConfigCollection("battle_pass/battlepassreward.json")]
public class BattlePassRewardConfig : IConfig
{
    public ConfigType Type => ConfigType.BattlePassReward;
    public int Identifier => Level;
    public int BattlePassId { get; set; }
    public int Level { get; set; }
    //public List<int> FreeReward { get; set; } = [];
   // public List<int> PayReward { get; set; } = [];
    public bool IsMilestone { get; set; }
}