using GameServer.Systems.Entity.Component;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Systems.Entity;
internal class AnimalEntity : EntityBase
{
    public AnimalEntity(long id, int configId, IGameActionListener listener) : base(id, listener)
    {
        ConfigId = configId;
        DynamicId = configId;

    }

    public int ConfigId { get; }

    public override EEntityType Type => EEntityType.Animal;
    public override EntityConfigType ConfigType => EntityConfigType.Level;

    public override void OnCreate()
    {
        base.OnCreate();

        EntityAttributeComponent attributeComponent = ComponentSystem.Create<EntityAttributeComponent>();
        attributeComponent.SetAttribute(EAttributeType.LifeMax, 100);
        attributeComponent.SetAttribute(EAttributeType.Life, 100);


       State = EntityState.Born;

        EntityMonsterAiComponent aiComponent = ComponentSystem.Create<EntityMonsterAiComponent>();
        aiComponent.AiTeamInitId = 100;

        EntityFsmComponent fsm = ComponentSystem.Create<EntityFsmComponent>();

        fsm.Fsms.Add(new DFsm
        {
            FsmId = 10007, // Main State Machine
            CurrentState = 10015 // Moving Combat
        });


    }

    public override EntityPb Pb
    {
        get
        {
            EntityPb pb = new()
            {
                Id = Id,
                EntityType = (int)Type,
                ConfigType = (int)ConfigType,
                EntityState = (int)State,
                ConfigId = ConfigId,
                Pos = Pos,
                Rot = Rot,
                LivingStatus = (int)LivingStatus,
                IsVisible = IsVisible,
                InitLinearVelocity = new(),
                InitPos = new()
            };

            pb.ComponentPbs.AddRange(ComponentSystem.Pb);

            return pb;
        }
    }
}
