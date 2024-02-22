using GameServer.Systems.Entity.Component;
using Protocol;

namespace GameServer.Systems.Entity;
internal class PlayerEntity : EntityBase
{
    public PlayerEntity(long id, int configId, int playerId) : base(id)
    {
        ConfigId = configId;
        PlayerId = playerId;
    }

    public int ConfigId { get; }
    public int PlayerId { get; }

    public bool IsCurrentRole { get; set; }

    public int WeaponId
    {
        get => ComponentSystem.Get<EntityEquipComponent>().WeaponId;
        set => ComponentSystem.Get<EntityEquipComponent>().WeaponId = value;
    }

    public int Health
    {
        get => ComponentSystem.Get<EntityAttributeComponent>().GetAttribute(EAttributeType.Life);
        set => ComponentSystem.Get<EntityAttributeComponent>().SetAttribute(EAttributeType.Life, value);
    }

    public int HealthMax
    {
        get => ComponentSystem.Get<EntityAttributeComponent>().GetAttribute(EAttributeType.LifeMax);
        set => ComponentSystem.Get<EntityAttributeComponent>().SetAttribute(EAttributeType.LifeMax, value);
    }

    public override void OnCreate()
    {
        base.OnCreate();

        // Should be created immediately
        EntityConcomitantsComponent concomitantsComponent = ComponentSystem.Create<EntityConcomitantsComponent>();
        concomitantsComponent.CustomEntityIds.Add(Id);

        EntityVisionSkillComponent visionSkillComponent = ComponentSystem.Create<EntityVisionSkillComponent>();
        visionSkillComponent.SetExploreTool(1001);

        _ = ComponentSystem.Create<EntityEquipComponent>();
        _ = ComponentSystem.Create<EntityAttributeComponent>();

        // TODO: temporary solution to enable glider and wall run, should implement proper buff management.
        EntityFightBuffComponent fightBuffComponent = ComponentSystem.Get<EntityFightBuffComponent>();
        fightBuffComponent.BuffInfoList.Add(new FightBuffInformation
        {
            BuffId = 3004,
            EntityId = Id,
            InstigatorId = Id,
            IsActive = true,
            Duration = -1,
            LeftDuration = -1,
            Level = 1,
            StackCount = 1
        });

        fightBuffComponent.BuffInfoList.Add(new FightBuffInformation
        {
            BuffId = 3003,
            EntityId = Id,
            InstigatorId = Id,
            IsActive = true,
            Duration = -1,
            LeftDuration = -1,
            Level = 1,
            StackCount = 1
        });
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override EEntityType Type => EEntityType.Player;
    public override EntityConfigType ConfigType => EntityConfigType.Character;

    public override bool IsVisible => IsCurrentRole;

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
                PlayerId = PlayerId,
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
