using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("quest/quest.json")]
public class QuestConfig : IConfig
{
    public ConfigType Type => ConfigType.Quest;
    public int Identifier => Id;
  
    public int Id { get; set; }
    public int QuestType { get; set; }
    public bool IsRepeat { get; set; }
    public bool IsAutoTrack { get; set; }
   // public required string QuestText { get; set; }
    public int IsOnline { get; set; }
    public int RewardShow { get; set; }
    public int AreaId { get; set; }
    public int Duration { get; set; }
    

}

//[ConfigCollection("questdata/questdata.json")]
//public class QuestDataConfig : IConfig
//{
//    public ConfigType Type => ConfigType.QuestData;
//    public int Identifier => Id;

//    public int  { get; set; }


//}



