using GameServer.Controllers.Attributes;
using GameServer.Controllers.Event;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class RoleController : Controller
{
    public RoleController(PlayerSession session) : base(session)
    {
        // RoleMessageHandler.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(ModelManager modelManager)
    {
        PlayerModel player = modelManager.Player;

        await Session.Push(MessageId.PbGetRoleListNotify, new PbGetRoleListNotify
        {
            RoleList =
            {
                new roleInfo
                {
                    RoleId = player.CharacterId,
                    Level = 1,
                }
            }
        });
    }

    [NetEvent(MessageId.RoleFavorListRequest)]
    public ResponseMessage OnRoleFavorListRequest() => Response(MessageId.RoleFavorListResponse, new RoleFavorListResponse());
}
