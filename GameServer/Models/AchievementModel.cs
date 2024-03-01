using Protocol;

namespace GameServer.Models;
internal class AchievementModel
{
    public List<AchievementGroupInfo> AchievementGroupInfoList { get; } = [];
    //public List<AchievementGroupEntry> AchievementGroupEntryList { get; } = [];
    public AchievementGroupEntry AchievementGroupEntryList { get; set; } = new();

    public List<AchievementEntry> AchievementEntryList { get; } = [];
    public AchievementProgress AchievementProgress { get; } = new()
    {
        CurProgress = 1,
        TotalProgress = 1
    };


    public void CleanAchievement()
    {
        AchievementEntryList.Clear();
        AchievementGroupEntryList = new();
    }




    public AchievementEntry AddAchievementEntry(int Achievementid,bool IsReceive, AchievementProgress progress)
    {
        AchievementEntry entry = new()
        {
            Id = Achievementid,
            FinishTime = 0,
            IsReceive = IsReceive,
            Progress = progress
        };
        AchievementEntryList.Add(entry);
        return entry;
    }

    public AchievementGroupInfo AddAchievementGroupInfo(AchievementGroupEntry GroupEntry, List<AchievementEntry> entry)
    {
        AchievementGroupInfo GroupInfo = new()
        {
            AchievementGroupEntry = GroupEntry,
            AchievementEntryList = { entry }

        };
        AchievementGroupInfoList.Add(GroupInfo);
        return GroupInfo;
    }

    public AchievementGroupEntry AddAchievementGroupEntry(int GroupId,bool IsReceive)
    {
        AchievementGroupEntry Groupentry = new()
        {
            Id = GroupId,
            FinishTime = 0,
            IsReceive = IsReceive
        };
        AchievementGroupEntryList = Groupentry;
        return Groupentry;
    }

}
