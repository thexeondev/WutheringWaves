using Protocol;

namespace GameServer.Models;

internal class LivenessModel
{
    public List<LivenessTask> LivenessTaskList { get; } = [];


    public void ClearLivenessTask()
    {
        LivenessTaskList.Clear();
    }

    public LivenessTask AddLivenessTask(int taskId, int current, int target,bool isFinished,bool isTaken,bool isConditionUnlock)
    {
        LivenessTask Task = new()
        {
            Id = taskId,
            Current = current,
            Target = target,
            IsFinished = isFinished,
            IsTaken = isTaken,
            IsConditionUnlock = isConditionUnlock
        };
        LivenessTaskList.Add(Task);
        return Task;
    }
}
