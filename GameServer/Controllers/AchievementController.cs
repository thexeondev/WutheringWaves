using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class AchievementController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;
    public AchievementController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        // AchievementController.
        _modelManager = modelManager;
        _configManager = configManager;
    }

    [NetEvent(MessageId.AchievementInfoRequest)]
    public RpcResult OnAchievementInfoRequest(/*AchievementInfoRequest request*/)
    {     
        _modelManager.Achievement.CleanAchievement();
        _modelManager.Achievement.AchievementGroupInfoList.Clear();
        foreach (AchievementGroupConfig achievementGroupConfig in _configManager.Enumerate<AchievementGroupConfig>())
        {
            _modelManager.Achievement.AddAchievementGroupEntry(achievementGroupConfig.Id, true);//AchievementEntryList
            foreach (AchievementConfig achievementConfig in _configManager.Enumerate<AchievementConfig>())
            {
                if (achievementGroupConfig.Id == achievementConfig.GroupId)
                {
                    _modelManager.Achievement.AddAchievementEntry(achievementConfig.Id, true, _modelManager.Achievement.AchievementProgress);
                }              
            }

            _modelManager.Achievement.AddAchievementGroupInfo(_modelManager.Achievement.AchievementGroupEntryList, _modelManager.Achievement.AchievementEntryList);
            _modelManager.Achievement.CleanAchievement();
        }

        //some id can't show  
        return Response(MessageId.AchievementInfoResponse, new AchievementInfoResponse
        {
            AchievementGroupInfoList = { _modelManager.Achievement.AchievementGroupInfoList }          
        });;
    }


    [NetEvent(MessageId.AchievementFinishRequest)]
    public RpcResult AchievementFinishRequest(AchievementFinishRequest request)
    {
        var itemid = request.Id;

        if (itemid != 0)
        {
            return Response(MessageId.AchievementFinishResponse, new AchievementFinishResponse
            {
                ErrorCode = 0
            });
        }
        else
        {
            return Response(MessageId.AchievementFinishResponse, new AchievementFinishResponse
            {
                ErrorCode = 3
            });
        }

    }

    [NetEvent(MessageId.AchievementReceiveRequest)]
    public RpcResult AchievementReceiveRequest(/*AchievementReceiveRequest request*/)
    {

        //bool isGroupId = request.IsGroupId;
        //int id = request.Id;
        return Response(MessageId.AchievementReceiveResponse, new AchievementReceiveResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            ErrorParams = { "AchievementReceiveResponseSuccess?" },
            ItemMap = { }

        });
    }

    //public async void AchievementGroupProgressNotify()
    //public async void AchievementProgressNotify()
    //public async void AchievementListProgressNotify()
}// AchievementController.


