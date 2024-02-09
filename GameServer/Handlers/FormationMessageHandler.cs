using GameServer.Handlers.Attributes;
using GameServer.Models;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class FormationMessageHandler : MessageHandlerBase
{
    private readonly ModelManager _modelManager;

    public FormationMessageHandler(PlayerSession session, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
    }

    [MessageHandler(MessageId.GetFormationDataRequest)]
    public async Task OnGetFormationDataRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.GetFormationDataResponse, new GetFormationDataResponse
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
}
