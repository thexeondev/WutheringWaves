using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Systems.Entity;
internal class EntitySystem
{
    private readonly List<EntityBase> _entities;
    private readonly IGameActionListener _listener;

    public EntitySystem(IGameActionListener listener)
    {
        _entities = [];
        _listener = listener;
    }

    public IEnumerable<EntityBase> EnumerateEntities()
    {
        return _entities;
    }

    public void Add(IEnumerable<EntityBase> entities)
    {
        foreach (EntityBase entity in entities)
        {
            if (_entities.Any(e => e.Id == entity.Id))
                throw new InvalidOperationException($"EntitySystem::Create - entity with id {entity.Id} already exists");

            _entities.Add(entity);
        }

        _ = _listener.OnEntitiesAdded(entities);
    }

    public void Destroy(IEnumerable<EntityBase> entities)
    {
        foreach (EntityBase entity in entities)
            _ = _entities.Remove(entity);

        _ = _listener.OnEntitiesRemoved(entities);
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
