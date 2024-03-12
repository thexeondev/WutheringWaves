using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Systems.Entity;
internal class EntityFactory
{
    private static readonly ObjectFactory<PlayerEntity> s_createPlayerEntity;
    private static readonly ObjectFactory<MonsterEntity> s_createMonsterEntity;
    private static readonly ObjectFactory<NpcEntity> s_createNpcEntity;
    private static readonly ObjectFactory<AnimalEntity> s_createAnimalEntity;
    private static readonly ObjectFactory<SceneItemEntity> s_createSceneItemEntity;
    private long _entityIdCounter;
    private readonly IServiceProvider _serviceProvider;

    static EntityFactory()
    {
        s_createPlayerEntity = ActivatorUtilities.CreateFactory<PlayerEntity>([typeof(long), typeof(int), typeof(int)]);
        s_createMonsterEntity = ActivatorUtilities.CreateFactory<MonsterEntity>([typeof(long), typeof(int)]);
        s_createNpcEntity = ActivatorUtilities.CreateFactory<NpcEntity>([typeof(long), typeof(int)]);
        s_createAnimalEntity = ActivatorUtilities.CreateFactory<AnimalEntity>([typeof(long), typeof(int)]);
        s_createSceneItemEntity = ActivatorUtilities.CreateFactory<SceneItemEntity>([typeof(long), typeof(int)]);
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

    public NpcEntity CreateNpc(int levelEntityId)
    {
        NpcEntity Npcentity = s_createNpcEntity(_serviceProvider, [NextId(), levelEntityId]);
        Npcentity.OnCreate();

        return Npcentity;
    }

    public AnimalEntity CreateAnimal(int levelEntityId)
    {
        AnimalEntity animalentity = s_createAnimalEntity(_serviceProvider, [NextId(), levelEntityId]);
        animalentity.OnCreate();

        return animalentity;
    }

    public SceneItemEntity CreateSceneItem(int levelEntityId)
    {
        SceneItemEntity sceneItemEntity = s_createSceneItemEntity(_serviceProvider, [NextId(), levelEntityId]);
        sceneItemEntity.OnCreate();

        return sceneItemEntity;
    }

    private long NextId() => Interlocked.Increment(ref _entityIdCounter);
}
