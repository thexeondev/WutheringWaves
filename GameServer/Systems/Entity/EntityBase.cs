using GameServer.Systems.Entity.Component;
using Protocol;

namespace GameServer.Systems.Entity;
internal abstract class EntityBase
{
    public long Id { get; }
    public EntityComponentSystem ComponentSystem { get; }

    public Vector Pos { get; set; }
    public Rotator Rot { get; set; }

    public bool Active { get; set; }

    public EntityState State { get; protected set; }

    public EntityBase(long id)
    {
        Id = id;

        Pos = new Vector();
        Rot = new Rotator();

        ComponentSystem = new EntityComponentSystem();
    }

    public virtual void OnCreate()
    {
        State = EntityState.Born;
    }

    public void Activate()
    {
        AddComponents();
    }

    public virtual void AddComponents()
    {
        // AddComponents.
    }

    public virtual LivingStatus LivingStatus => LivingStatus.Alive;
    public virtual bool IsVisible => true;

    public abstract EEntityType Type { get; }
    public abstract EntityConfigType ConfigType { get; }

    public abstract EntityPb Pb { get; }
}
