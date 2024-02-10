using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class FunctionalController : Controller
{
    private static readonly int[] s_functions = [10001, 10002, 10003, 10004, 10005, 10006, 10007, 10008, 10009, 10010, 10011, 10012, 10013, 10014, 10015, 10016, 10017, 10018, 10019, 10020, 10021, 10022, 10023, 10024, 10025, 10026, 10027, 10028, 10029, 10030, 10031, 10033, 10034, 10035, 10036, 10041, 10042, 10043, 10046, 10047, 10048, 10049, 10050, 10052, 10023001, 10023002, 10023004, 10023005, 10053, 10054, 10001003, 10055, 10026001, 10026002, 10026003, 10026004, 10026005, 10026006, 10026008, 10056, 10026101, 110057, 10001004, 10037, 10057, 10059, 10058, 10023003, 10032, 110056, 110058, 10060, 10061];

    public FunctionalController(PlayerSession session) : base(session)
    {
        // FunctionalController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        FuncOpenNotify notify = new();

        foreach (int id in s_functions)
        {
            notify.Func.Add(new Function
            {
                Id = id,
                Flag = 2
            });
        }

        await Session.Push(MessageId.FuncOpenNotify, notify);
    }
}
