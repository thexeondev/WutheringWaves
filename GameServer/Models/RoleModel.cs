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
            Level = 90,
            Breakthrough= 6,
            ResonantChainGroupIndex =6,

        };

        Roles.Add(info);
        return info;
    }

    public roleInfo? GetRoleById(int roleId)
    {
        return Roles.SingleOrDefault(role => role.RoleId == roleId);
    }
}
