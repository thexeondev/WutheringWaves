namespace GameServer.Models;
internal class FormationModel
{
    public int[] RoleIds { get; }

    public FormationModel()
    {
        RoleIds = new int[3];
    }

    public void Set(int[] roleIds)
    {
        for (int i = 0; i < roleIds.Length; i++)
        {
            RoleIds[i] = roleIds[i];
        }
    }
}
