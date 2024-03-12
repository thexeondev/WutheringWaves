using Core.Config;
using GameServer.Network;
using GameServer.Systems.Notify;
using Protocol;


namespace GameServer.Models;
internal class RoleModel
{

    public List<roleInfo> Roles { get; } = [];

    public  roleInfo DebugCreate(int roleId)
    {

        roleInfo info = new()
        {
            RoleId = roleId,
            Level = 90,
            Breakthrough = 6,
            ResonantChainGroupIndex = 6,


        };

        Roles.Add(info);
        return info;
    }

    public roleInfo? GetRoleById(int roleId)
    {
        return Roles.SingleOrDefault(role => role.RoleId == roleId);
    }
}
