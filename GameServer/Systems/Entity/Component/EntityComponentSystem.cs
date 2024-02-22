using System.Diagnostics.CodeAnalysis;
using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityComponentSystem
{
    private readonly List<EntityComponentBase> _components;

    public EntityComponentSystem()
    {
        _components = [];
    }

    public TEntityComponent Create<TEntityComponent>() where TEntityComponent : EntityComponentBase, new()
    {
        if (_components.Any(component => component is TEntityComponent)) throw new InvalidOperationException($"Component of type {nameof(TEntityComponent)} already exists");

        TEntityComponent component = new();
        _components.Add(component);

        return component;
    }

    public TEntityComponent Get<TEntityComponent>() where TEntityComponent : EntityComponentBase
    {
        return (_components.Single(component => component is TEntityComponent) as TEntityComponent)!;
    }

    public bool TryGet<TEntityComponent>([NotNullWhen(true)] out TEntityComponent? component) where TEntityComponent : EntityComponentBase
    {
        return (component = _components.SingleOrDefault(component => component is TEntityComponent) as TEntityComponent) != null;
    }

    public IEnumerable<EntityComponentPb> Pb => _components.Select(component => component.Pb);
}
