using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class ActivityController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;


    //private ActivityData  ActivityData { get;set} = new();
    public ActivityController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
        _configManager = configManager;

        //    ActivityDisableNotify = 11705,
        //ActivityFirstReadRequest = 11702,
        //ActivityFirstReadResponse = 11703,
        //ActivityRequest = 11700,
        //ActivityResponse = 11701,
        //ActivityUpdateNotify = 11704,
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task ActivityUpdate()
    {
        foreach (ActivityConfig activityConfig in _configManager.Enumerate<ActivityConfig>())
        {
            _modelManager.Activity.AddActivity(activityConfig.Id, activityConfig.ActivityType, activityConfig.PreShowGuideQuest);
        }

        ActivityUpdateNotify notify = new();
        {
            notify.Activities.Add(_modelManager.Activity.ActivityDataList);
        }
        await Session.Push(MessageId.ActivityUpdateNotify, notify);
    }


    [NetEvent(MessageId.ActivityRequest)]
    public RpcResult OnActivityRequest(/*ActivityRequest request*/)
    {

        ActivityResponse response = new();
        {
            response.ErrorCode = 0;
            response.Activities.Add(_modelManager.Activity.ActivityDataList);
        }
        return Response(MessageId.ActivityResponse, response);
    }
    //LivenessCountUpdateNotify = 11906,
    //LivenessRefreshNotify = 11895,
    //LivenessRequest = 11892,
    //LivenessResponse = 11893,
    //LivenessTakeRequest = 11898,
    //LivenessTakeResponse = 11899,
    //LivenessTaskTakeRequest = 11896,
    //LivenessTaskTakeResponse = 11897,
    //LivenessUpdateNotify = 11894,
    [GameEvent(GameEventType.EnterGame)]
    public async Task LivenessRefreshNotify()
    {
        _modelManager.Liveness.ClearLivenessTask();
        foreach (LivenesstaskConfig livenesstaskConfig in _configManager.Enumerate<LivenesstaskConfig>())
        {
            _modelManager.Liveness.AddLivenessTask(livenesstaskConfig.TaskId, 1, 1, true, false, true);
        }
        LivenessRefreshNotify notify = new();
        {
            notify.LivenessInfo = new()
            {
                LivenessCount = 200,
                DayEnd = (Int64)86400000,
                RewardedLiveness = { 50002, 50003, 50004, 50005, 50006 },
                Tasks = { _modelManager.Liveness.LivenessTaskList }
            };
        }
        await Session.Push(MessageId.LivenessRefreshNotify, notify);
    }

    //public async Task LivenessCountUpdateNotify()
    //{

    //    LivenessCountUpdateNotify notify = new();
    //    {
    //        notify.LivenessCount = 100;
    //    }
    //    await Session.Push(MessageId.LivenessCountUpdateNotify, notify);
    //}
    //[NetEvent(MessageId.LivenessRequest)]
    //public RpcResult OnLivenessRequest(/*LivenessRequest request*/) => Response(MessageId.LivenessResponse, new LivenessResponse());


    [NetEvent(MessageId.LivenessTakeRequest)]
    public RpcResult OnLivenessTakeRequest(/*ActivityRequest request*/)
    {


        return Response(MessageId.LivenessTakeResponse, new LivenessTakeResponse 
        { 
            ErrorCode = 0,
            Ids = { },
            ItemMap = { },      
        });
    }



}

