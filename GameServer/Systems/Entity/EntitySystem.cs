using Protocol;

namespace GameServer.Systems.Entity;
internal class EntitySystem
{
    private readonly List<EntityBase> _entities;

    public EntitySystem()
    {
        _entities = [];
    }

    public IEnumerable<EntityBase> EnumerateEntities()
    {
        return _entities;
    }

    public void Create(EntityBase entity)
    {
        if (_entities.Any(e => e.Id == entity.Id)) 
            throw new InvalidOperationException($"EntitySystem::Create - entity with id {entity.Id} already exists");

        entity.OnCreate();
        _entities.Add(entity);
    }

    public void Destroy(EntityBase entity)
    {
        _ = _entities.Remove(entity);
    }

    public void Activate(EntityBase entity)
    {
        entity.Activate();
    }

    public TEntity? Get<TEntity>(long id) where TEntity : EntityBase
    {
        return _entities.SingleOrDefault(e => e.Id == id) as TEntity;
    }

    public IEnumerable<EntityPb> Pb => _entities.Select(e => e.Pb);
}
