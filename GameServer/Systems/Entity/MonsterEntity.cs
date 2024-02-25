using GameServer.Systems.Entity.Component;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Systems.Entity;
internal class MonsterEntity : EntityBase
{
    public MonsterEntity(long id, int configId, IGameActionListener listener) : base(id, listener)
    {
        ConfigId = configId;
        DynamicId = configId;
    }

    public int ConfigId { get; }

    public override EEntityType Type => EEntityType.Monster;
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
            CurrentState = 10013 // Battle Branching
        });

        fsm.Fsms.Add(new DFsm
        {
            FsmId = 10007, // Main State Machine
            CurrentState = 10015 // Moving Combat
        });

        // Some monsters need weapon
        fsm.Fsms.Add(new DFsm
        {
            FsmId = 100,
            CurrentState = 9 // [9 - Empty hand, 10 - Crowbar, 11 - flamethrower, 12 - chainsaw, 13 - electric blade, 14 - sniper rifle]
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
