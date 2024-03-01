using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Systems.Event;
using Protocol;



namespace GameServer.Controllers;
internal class MonthCardController : Controller
{
    public MonthCardController(PlayerSession session) : base(session)
    {
        // MonthCardController.
        //    MonthCardDailyRewardNotify = 10226,
        //MonthCardRequest = 10224,
        //MonthCardResponse = 10225,
        //MonthCardUseNotify = 10227,
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        await Session.Push(MessageId.MonthCardDailyRewardNotify, new MonthCardDailyRewardNotify
        {
            ItemId = 1,
            Count = 300,
            Days = 30
        });
        //await Session.Push(MessageId.MonthCardDailyRewardNotify, new MonthCardDailyRewardNotify
        //{
        //    ItemId = 2,
        //    Count = 0,
        //    Days = 7
        //});
        //await Session.Push(MessageId.MonthCardDailyRewardNotify, new MonthCardDailyRewardNotify
        //{
        //    ItemId = 3,
        //    Count = 0,
        //    Days = 30
        //});
        //await Session.Push(MessageId.MonthCardDailyRewardNotify, new MonthCardDailyRewardNotify
        //{
        //    ItemId = 4,
        //    Count = 0,
        //    Days = 15
        //});
    }

    [NetEvent(MessageId.MonthCardRequest)]
    public RpcResult OnMonthCardRequest( /*MonthCardRequest request*/) /*=> Response(MessageId.MonthCardResponse, new MonthCardResponse());*/
    {
        return Response(MessageId.MonthCardResponse, new MonthCardResponse
        {
            Days = 30,
            IsDailyGot = true,
            ErrorCode = (int)ErrorCode.Success
        });
    }



}










