using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class RoleController : Controller
{
    public RoleController(PlayerSession session) : base(session)
    {
        // RoleController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(ModelManager modelManager)
    {
        PlayerModel player = modelManager.Player;

        await Session.Push(MessageId.PbGetRoleListNotify, new PbGetRoleListNotify
        {
            RoleList =
            {
                player.Characters.Select(i => new roleInfo
                {
                    RoleId = i,
                    Level = 1
                })
            }
        });
    }

    [NetEvent(MessageId.SwitchRoleRequest)]
    public ResponseMessage OnSwitchRoleRequest(SwitchRoleRequest request)
    {
        return Response(MessageId.SwitchRoleResponse, new SwitchRoleResponse
        {
            RoleId = request.RoleId
        });
    }

    [NetEvent(MessageId.RoleFavorListRequest)]
    public ResponseMessage OnRoleFavorListRequest() => Response(MessageId.RoleFavorListResponse, new RoleFavorListResponse());
}
