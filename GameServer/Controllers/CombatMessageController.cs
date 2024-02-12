using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class CombatMessageController : Controller
{
    public CombatMessageController(PlayerSession session) : base(session)
    {
        // CombatMessageController.
    }

    [NetEvent(MessageId.CombatSendPackRequest)] // TODO: CombatSendPackRequest is important
    public ResponseMessage OnCombatSendPackRequest() => Response(MessageId.CombatSendPackResponse, new CombatSendPackResponse());
}
