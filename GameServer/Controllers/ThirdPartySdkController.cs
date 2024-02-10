using GameServer.Controllers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;
internal class ThirdPartySdkController : Controller
{
    public ThirdPartySdkController(PlayerSession session) : base(session)
    {
        // ThirdPartySdkController.
    }

    [NetEvent(MessageId.AceAntiDataPush)]
    public void OnAceAntiDataPush()
    {
        // This is just annoying.
    }
}
