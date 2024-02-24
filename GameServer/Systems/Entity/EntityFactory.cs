using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Systems.Entity;
internal class EntityFactory
{
    private static readonly ObjectFactory<PlayerEntity> s_createPlayerEntity;
    private static readonly ObjectFactory<MonsterEntity> s_createMonsterEntity;

    private long _entityIdCounter;
    private readonly IServiceProvider _serviceProvider;

    static EntityFactory()
    {
        s_createPlayerEntity = ActivatorUtilities.CreateFactory<PlayerEntity>([typeof(long), typeof(int), typeof(int)]);
        s_createMonsterEntity = ActivatorUtilities.CreateFactory<MonsterEntity>([typeof(long), typeof(int)]);
    }

    public EntityFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public PlayerEntity CreatePlayer(int characterConfigId, int playerId)
    {
        PlayerEntity entity = s_createPlayerEntity(_serviceProvider, [NextId(), characterConfigId, playerId]);
        entity.OnCreate();

        return entity;
    }

    public MonsterEntity CreateMonster(int levelEntityId)
    {
        MonsterEntity monsterEntity = s_createMonsterEntity(_serviceProvider, [NextId(), levelEntityId]);
        monsterEntity.OnCreate();

        return monsterEntity;
    }

    private long NextId() => Interlocked.Increment(ref _entityIdCounter);
}
