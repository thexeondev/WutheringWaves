namespace GameServer.Systems.Entity;
internal class EntityFactory
{
    private long _entityIdCounter;

    public PlayerEntity CreatePlayer(int characterConfigId, int playerId)
        => new(NextId(), characterConfigId, playerId);

    private long NextId() => Interlocked.Increment(ref _entityIdCounter);
}
