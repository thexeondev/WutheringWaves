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
    public async Task OnEnterGame()
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


    [NetEvent(MessageId.LivenessRequest)]
    public RpcResult OnLivenessRequest() => Response(MessageId.LivenessResponse, new LivenessResponse());
}

