using Protocol;


namespace GameServer.Models;
internal class QuestModel
{
    public List<QuestInfo> QuestInfoList { get; } = [];

    public List<int> QuestIdList { get; } = [];


    public void ClearQuest()
    {
        QuestInfoList.Clear();
    }

    public QuestInfo AddQuest(int questId, int status)
    {

        QuestInfo questInfo = new()
        {
            QuestId = questId,
            Status = status,
        };
        QuestInfoList.Add(questInfo);
        return questInfo;
    }

    public int AddQuestids(int questId)
    {

        int questInfo = questId;

        QuestIdList.Add(questInfo);
        return questInfo;
    }

}
