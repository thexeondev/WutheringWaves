using GameServer.Controllers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;
internal class FormationAttributeController : Controller
{
    public FormationAttributeController(PlayerSession session) : base(session)
    {
        // FormationAttributeController.
    }

    [NetEvent(MessageId.TimeCheckRequest)]
    public RpcResult OnTimeCheckRequest() => Response(MessageId.TimeCheckResponse, new TimeCheckResponse());

    [NetEvent(MessageId.FormationAttrRequest)]
    public RpcResult OnFormationAttrRequest() => Response(MessageId.FormationAttrResponse, new FormationAttrResponse());
}
