using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Entity;
using GameServer.Systems.Event;
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
                    CurRole = _modelManager.Player.Characters[0],
                    FormationId = 1,
                    IsCurrent = true,
                    RoleIds = { _modelManager.Player.Characters },
                }
            },
    });

    [NetEvent(MessageId.UpdateFormationRequest)]
    public async Task<ResponseMessage> OnUpdateFormationRequest(UpdateFormationRequest request, EventSystem eventSystem)
    {
        _modelManager.Formation.Set([.. request.Formation.RoleIds]);
        await eventSystem.Emit(GameEventType.FormationUpdated);

        return Response(MessageId.UpdateFormationResponse, new UpdateFormationResponse
        {
            Formation = request.Formation
        });
    }
}
