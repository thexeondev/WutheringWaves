using Protocol;

namespace GameServer.Models;

internal class ActivityModel
{
    public List<ActivityData> ActivityDataList { get; } = [];
    

    public void ClearActivity()
    {
        ActivityDataList.Clear();
    }

    public ActivityData AddActivity(int ActivityId, int Type, List<int> PreQuests) 
    {
        ActivityData Activity = new()
        {
            Id = ActivityId,
            Type = Type,
            BeginShowTime = 0,
            EndShowTime = Int32.MaxValue,
            BeginOpenTime = 0,
            EndOpenTime = Int32.MaxValue,
            IsUnlock = true,
            IsFirstOpen = true,
            CompletePreQuests = { PreQuests }
        };
        ActivityDataList.Add(Activity);
        return Activity;
    }
}
