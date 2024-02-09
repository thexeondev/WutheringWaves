using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class FormationController : Controller
{
    private readonly ModelManager _modelManager;

    public FormationController(PlayerSession session, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
    }

    [NetEvent(MessageId.GetFormationDataRequest)]
    public ResponseMessage OnGetFormationDataRequest() => Response(MessageId.GetFormationDataResponse, new GetFormationDataResponse
    {
        Formations =
            {
                new FightFormation
                {
                    CurRole = _modelManager.Player.CharacterId,
                    FormationId = 1,
                    IsCurrent = true,
                    RoleIds = { _modelManager.Player.CharacterId },
                }
            },
    });
}
