using Core.Config;
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
    public async Task OnEnterGame(ModelManager modelManager, ConfigManager configManager)
    {
        PlayerModel player = modelManager.Player;

        await Session.Push(MessageId.PbGetRoleListNotify, new PbGetRoleListNotify
        {
            RoleList =
            {
                configManager.GetCollection(ConfigType.RoleInfo)
                .Enumerate<RoleInfoConfig>()
                .Select(config => new roleInfo
                {
                    RoleId = config.Id,
                    Level = 1
                })
            }
        });
    }

    [NetEvent(MessageId.SwitchRoleRequest)]
    public async Task<ResponseMessage> OnSwitchRoleRequest(SwitchRoleRequest request, CreatureController creatureController)
    {
        await creatureController.SwitchPlayerEntity(request.RoleId);
        return Response(MessageId.SwitchRoleResponse, new SwitchRoleResponse
        {
            RoleId = request.RoleId
        });
    }

    [NetEvent(MessageId.RoleFavorListRequest)]
    public ResponseMessage OnRoleFavorListRequest() => Response(MessageId.RoleFavorListResponse, new RoleFavorListResponse());
}
