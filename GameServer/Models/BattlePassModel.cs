using Core.Config;
using Protocol;

namespace GameServer.Models;

internal class BattlePassModel
{
    public List<PbBattlePassTask> PbBattlePassTaskList { get; } = [];
    public List<PbBattlePassReward> PbBattlePassRewardList0 { get; } = [];
    public List<PbBattlePassReward> PbBattlePassRewardList1 { get; } = [];
    public List<PbBattlePassRecurringReward> PbBattlePassRecurringRewardList0 { get; } = [];
    public List<PbBattlePassRecurringReward> PbBattlePassRecurringRewardList1 { get; } = [];

    public void ClearBattlePassTask()
    {
        PbBattlePassTaskList.Clear();
    }

    public void ClearBattlePassReward()
    {
        PbBattlePassRewardList0.Clear();
        PbBattlePassRewardList1.Clear();
        PbBattlePassRecurringRewardList0.Clear();
        PbBattlePassRecurringRewardList1.Clear();
    }

    public PbBattlePassTask AddBattlePassTask(int id, int current, int target, bool isFinished, bool isTaken)
    {
        PbBattlePassTask pbBattlePassTask = new()
        {
            Id = id,
            Current = current,
            Target = target,
            IsFinished = isFinished,
            IsTaken = isTaken
        };
        PbBattlePassTaskList.Add(pbBattlePassTask);
        return pbBattlePassTask;
    }


    public PbBattlePassReward AddBattlePassReward(int level, int itemId, int type)
    {
        PbBattlePassReward pbBattlePassReward = new()
        {
            Level = level,
            ItemId = itemId,
            Type = type
        };
        if (type == 0)
            PbBattlePassRewardList0.Add(pbBattlePassReward);
        else
            PbBattlePassRewardList1.Add(pbBattlePassReward);
        return pbBattlePassReward;
    }


    public PbBattlePassRecurringReward AddBattlePassRecurringReward(int type, int itemId, int count)
    {
        PbBattlePassRecurringReward pbBattlePassRecurringReward = new()
        {
            Type = type,
            Count = count,
            ItemId = itemId,
        };
        if (type == 0)
            PbBattlePassRecurringRewardList0.Add(pbBattlePassRecurringReward);
        else
            PbBattlePassRecurringRewardList1.Add(pbBattlePassRecurringReward);
        return pbBattlePassRecurringReward;
    }

}

