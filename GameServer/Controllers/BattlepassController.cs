using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class BattlepassController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;

    public BattlepassController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        // BattlepassController.
        _modelManager = modelManager;
        _configManager = configManager;
        //    BattlePassEnterPush = 12052,
        //BattlePassExpUpdateNotify = 10707,
        //BattlePassPaidNotify = 10702,
        //BattlePassRecurringTakeRequest = 10713,
        //BattlePassRecurringTakeResponse = 10714,
        //BattlePassTakeAllRewardRequest = 10705,
        //BattlePassTakeAllRewardResponse = 10706,
        //BattlePassTakeRewardRequest = 10703,
        //BattlePassTakeRewardResponse = 10704,
        //BattlePassTaskRequest = 10708,
        //BattlePassTaskResponse = 10709,
        //BattlePassTaskTakeRequest = 10711,
        //BattlePassTaskTakeResponse = 10712,
        //BattlePassTaskUpdateNotify = 10710,
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task BattlePassTaskUpdate()
    {
        _modelManager.BattlePass.ClearBattlePassTask();
        foreach (BattlePassTaskConfig battlepasstaskconfig in _configManager.Enumerate<BattlePassTaskConfig>())
        {
            _modelManager.BattlePass.AddBattlePassTask(battlepasstaskconfig.TaskId, /*current*/70, /*target*/70,/*isfinished*/ true, /*istaken*/true);
        }
        BattlePassTaskUpdateNotify notify = new()
        {
            Tasks =
            {_modelManager.BattlePass.PbBattlePassTaskList}
        };
    await Session.Push(MessageId.BattlePassTaskUpdateNotify, notify);
}

    [GameEvent(GameEventType.EnterGame)]
    public async Task BattlePassPaid()
    {
        BattlePassPaidNotify notify = new()
        {
            PayStatus = (int)BattlePassPayStatus.Advanced
        };
        await Session.Push(MessageId.BattlePassPaidNotify, notify);
    }


    [NetEvent(MessageId.BattlePassRequest)]
    public RpcResult OnBattlePassRequest(/*BattlePassRequest request*/)
    {
        _modelManager.BattlePass.ClearBattlePassReward();
        foreach (BattlePassRewardConfig battlepassrewardconfig in _configManager.Enumerate<BattlePassRewardConfig>())
        {
            _modelManager.BattlePass.AddBattlePassReward(battlepassrewardconfig.Level, /*itemid*/1, /*type*/0);
            _modelManager.BattlePass.AddBattlePassReward(battlepassrewardconfig.Level, /*itemid*/1, /*type*/1);
            _modelManager.BattlePass.AddBattlePassRecurringReward(battlepassrewardconfig.Level, /*itemid*/1, /*count*/1);
            _modelManager.BattlePass.AddBattlePassRecurringReward(battlepassrewardconfig.Level, /*itemid*/1, /*count*/1);
        }



        return Response(MessageId.BattlePassResponse, new BattlePassResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            BattlePass = new PbBattlePass
            {
                InTimeRange = true,
                Id = 1,
                Level = 70,
                Exp = 43000,
                WeeklyTotalExp = 10000,
                PayStatus = (int)BattlePassPayStatus.Advanced,
                TakenRewards ={ _modelManager.BattlePass.PbBattlePassRewardList0, _modelManager.BattlePass.PbBattlePassRewardList1 },
                BeginTime = 0,
                EndTime = Int32.MaxValue,
                RecurringRewards ={ _modelManager.BattlePass.PbBattlePassRecurringRewardList0, _modelManager.BattlePass.PbBattlePassRecurringRewardList1 },
                HadEnter = true,               
            }
        });
    }

    [NetEvent(MessageId.BattlePassTaskTakeRequest)]
    public RpcResult OnBattlePassTaskTakeRequest(BattlePassTaskTakeRequest request)
    {
        
        return Response(MessageId.BattlePassTaskTakeResponse, new BattlePassTaskTakeResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            Ids = { request.Ids }
        });
    }


}
