namespace GameServer.Models;
internal class CreatureModel
{
    public int OwnerId { get; }

    public int InstanceId { get; private set; }
    public bool LoadingWorld { get; private set; }

    public long PlayerEntityId { get; set; }

    public CreatureModel(int ownerId)
    {
        OwnerId = ownerId;
    }

    public void SetSceneLoadingData(int instanceId)
    {
        InstanceId = instanceId;
        LoadingWorld = true;
    }

    public void OnWorldDone()
    {
        LoadingWorld = false;
    }
}
