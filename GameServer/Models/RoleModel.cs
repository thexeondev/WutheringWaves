using Core.Config;
using Protocol;

namespace GameServer.Models;
internal class RoleModel
{
    public List<roleInfo> Roles { get; } = [];

    public roleInfo Create(int id)
    {
        roleInfo info = new()
        {
            RoleId = id,
            Level = 1,
        };

        Roles.Add(info);
        return info;
    }

    public roleInfo? GetRoleById(int roleId)
    {
        return Roles.SingleOrDefault(role => role.RoleId == roleId);
    }
}
