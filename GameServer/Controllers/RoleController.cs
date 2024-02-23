using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class RoleController : Controller
{
    public RoleController(PlayerSession session) : base(session)
    {
        // RoleController.
    }

    [GameEvent(GameEventType.DebugUnlockAllRoles)]
    public void UnlockAllRoles(ConfigManager configManager, ModelManager modelManager)
    {
        foreach (RoleInfoConfig roleConfig in configManager.Enumerate<RoleInfoConfig>())
        {
            roleInfo role = modelManager.Roles.Create(roleConfig.Id);
            role.BaseProp.AddRange(CreateBasePropList(configManager.GetConfig<BasePropertyConfig>(roleConfig.Id)));

            WeaponItem weapon = modelManager.Inventory.AddNewWeapon(roleConfig.InitWeaponItemId);
            weapon.RoleId = role.RoleId;

            role.ApplyWeaponProperties(configManager.GetConfig<WeaponConfig>(weapon.Id)!);
        }
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(ModelManager modelManager)
    {
        await Session.Push(MessageId.PbGetRoleListNotify, new PbGetRoleListNotify
        {
            RoleList =
            {
                modelManager.Roles.Roles
            }
        });
    }

    [NetEvent(MessageId.SwitchRoleRequest)]
    public async Task<RpcResult> OnSwitchRoleRequest(SwitchRoleRequest request, CreatureController creatureController)
    {
        await creatureController.SwitchPlayerEntity(request.RoleId);
        return Response(MessageId.SwitchRoleResponse, new SwitchRoleResponse
        {
            RoleId = request.RoleId
        });
    }

    [NetEvent(MessageId.RoleFavorListRequest)]
    public RpcResult OnRoleFavorListRequest() => Response(MessageId.RoleFavorListResponse, new RoleFavorListResponse());

    [NetEvent(MessageId.ResonantChainUnlockRequest)]
    public RpcResult OnResonantChainUnlockRequest(ResonantChainUnlockRequest request, ModelManager modelManager, ConfigManager configManager)
    {
        roleInfo? role = modelManager.Roles.Roles.Find(r => r.RoleId == request.RoleId)!;
    
        if (role != null)
        {
            RoleInfoConfig roleConfig = configManager.GetConfig<RoleInfoConfig>(request.RoleId)!;
    
            int resonantChainGroupId = roleConfig.ResonantChainGroupId;
    
            // Todo: add buff by _resonantChainGroupId
    
            int curr = role.ResonantChainGroupIndex;
            int next = Math.Min(curr + 1, 6);
            role.ResonantChainGroupIndex = next;
    
            return Response(MessageId.ResonantChainUnlockResponse, new ResonantChainUnlockResponse
            {
                RoleId = request.RoleId,
                ResonantChainGroupIndex = next
            });
        }
    
        return Response(MessageId.ResonantChainUnlockResponse, new ResonantChainUnlockResponse
        {
            ErrCode = (int)ErrorCode.ErrRoleResonNotActive
        });
    }

    private static List<ArrayIntInt> CreateBasePropList(BasePropertyConfig? config)
    {
        List<ArrayIntInt> baseProp = [];
        if (config == null) return baseProp;

        baseProp.Add(new() { Key = (int)EAttributeType.Lv, Value = config.Lv });
        baseProp.Add(new() { Key = (int)EAttributeType.LifeMax, Value = config.LifeMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Life, Value = config.Life });
        baseProp.Add(new() { Key = (int)EAttributeType.Sheild, Value = config.Sheild });
        baseProp.Add(new() { Key = (int)EAttributeType.SheildDamageChange, Value = config.SheildDamageChange });
        baseProp.Add(new() { Key = (int)EAttributeType.SheildDamageReduce, Value = config.SheildDamageReduce });
        baseProp.Add(new() { Key = (int)EAttributeType.Atk, Value = config.Atk });
        baseProp.Add(new() { Key = (int)EAttributeType.Crit, Value = config.Crit });
        baseProp.Add(new() { Key = (int)EAttributeType.CritDamage, Value = config.CritDamage });
        baseProp.Add(new() { Key = (int)EAttributeType.Def, Value = config.Def });
        baseProp.Add(new() { Key = (int)EAttributeType.EnergyEfficiency, Value = config.EnergyEfficiency });
        baseProp.Add(new() { Key = (int)EAttributeType.CdReduse, Value = config.CdReduse });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionEfficiency, Value = config.ReactionEfficiency });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeNormalSkill, Value = config.DamageChangeNormalSkill });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChange, Value = config.DamageChange });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduce, Value = config.DamageReduce });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeAuto, Value = config.DamageChangeAuto });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeCast, Value = config.DamageChangeCast });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeUltra, Value = config.DamageChangeUltra });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeQte, Value = config.DamageChangeQte });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangePhys, Value = config.DamageChangePhys });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement1, Value = config.DamageChangeElement1 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement2, Value = config.DamageChangeElement2 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement3, Value = config.DamageChangeElement3 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement4, Value = config.DamageChangeElement4 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement5, Value = config.DamageChangeElement5 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangeElement6, Value = config.DamageChangeElement6 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistancePhys, Value = config.DamageResistancePhys });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement1, Value = config.DamageResistanceElement1 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement2, Value = config.DamageResistanceElement2 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement3, Value = config.DamageResistanceElement3 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement4, Value = config.DamageResistanceElement4 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement5, Value = config.DamageResistanceElement5 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageResistanceElement6, Value = config.DamageResistanceElement6 });
        baseProp.Add(new() { Key = (int)EAttributeType.HealChange, Value = config.HealChange });
        baseProp.Add(new() { Key = (int)EAttributeType.HealedChange, Value = config.HealedChange });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReducePhys, Value = config.DamageReducePhys });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement1, Value = config.DamageReduceElement1 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement2, Value = config.DamageReduceElement2 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement3, Value = config.DamageReduceElement3 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement4, Value = config.DamageReduceElement4 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement5, Value = config.DamageReduceElement5 });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageReduceElement6, Value = config.DamageReduceElement6 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange1, Value = config.ReactionChange1 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange2, Value = config.ReactionChange2 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange3, Value = config.ReactionChange3 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange4, Value = config.ReactionChange4 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange5, Value = config.ReactionChange5 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange6, Value = config.ReactionChange6 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange7, Value = config.ReactionChange7 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange8, Value = config.ReactionChange8 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange9, Value = config.ReactionChange9 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange10, Value = config.ReactionChange10 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange11, Value = config.ReactionChange11 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange12, Value = config.ReactionChange12 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange13, Value = config.ReactionChange13 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange14, Value = config.ReactionChange14 });
        baseProp.Add(new() { Key = (int)EAttributeType.ReactionChange15, Value = config.ReactionChange15 });
        baseProp.Add(new() { Key = (int)EAttributeType.EnergyMax, Value = config.EnergyMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Energy, Value = config.Energy });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy1Max, Value = config.SpecialEnergy1Max });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy1, Value = config.SpecialEnergy1 });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy2Max, Value = config.SpecialEnergy2Max });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy2, Value = config.SpecialEnergy2 });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy3Max, Value = config.SpecialEnergy3Max });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy3, Value = config.SpecialEnergy3 });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy4Max, Value = config.SpecialEnergy4Max });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialEnergy4, Value = config.SpecialEnergy4 });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthMax, Value = config.StrengthMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Strength, Value = config.Strength });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthRecover, Value = config.StrengthRecover });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthPunishTime, Value = config.StrengthPunishTime });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthRun, Value = config.StrengthRun });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthSwim, Value = config.StrengthSwim });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthFastSwim, Value = config.StrengthFastSwim });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthClimb, Value = config.StrengthClimb });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthFastClimb, Value = config.StrengthFastClimb });
        baseProp.Add(new() { Key = (int)EAttributeType.HardnessMax, Value = config.HardnessMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Hardness, Value = config.Hardness });
        baseProp.Add(new() { Key = (int)EAttributeType.HardnessRecover, Value = config.HardnessRecover });
        baseProp.Add(new() { Key = (int)EAttributeType.HardnessPunishTime, Value = config.HardnessPunishTime });
        baseProp.Add(new() { Key = (int)EAttributeType.HardnessChange, Value = config.HardnessChange });
        baseProp.Add(new() { Key = (int)EAttributeType.HardnessReduce, Value = config.HardnessReduce });
        baseProp.Add(new() { Key = (int)EAttributeType.RageMax, Value = config.RageMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Rage, Value = config.Rage });
        baseProp.Add(new() { Key = (int)EAttributeType.RageRecover, Value = config.RageRecover });
        baseProp.Add(new() { Key = (int)EAttributeType.RagePunishTime, Value = config.RagePunishTime });
        baseProp.Add(new() { Key = (int)EAttributeType.RageChange, Value = config.RageChange });
        baseProp.Add(new() { Key = (int)EAttributeType.RageReduce, Value = config.RageReduce });
        baseProp.Add(new() { Key = (int)EAttributeType.ToughMax, Value = config.ToughMax });
        baseProp.Add(new() { Key = (int)EAttributeType.Tough, Value = config.Tough });
        baseProp.Add(new() { Key = (int)EAttributeType.ToughRecover, Value = config.ToughRecover });
        baseProp.Add(new() { Key = (int)EAttributeType.ToughChange, Value = config.ToughChange });
        baseProp.Add(new() { Key = (int)EAttributeType.ToughReduce, Value = config.ToughReduce });
        baseProp.Add(new() { Key = (int)EAttributeType.ToughRecoverDelayTime, Value = config.ToughRecoverDelayTime });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower1, Value = config.ElementPower1 });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower2, Value = config.ElementPower2 });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower3, Value = config.ElementPower3 });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower4, Value = config.ElementPower4 });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower5, Value = config.ElementPower5 });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPower6, Value = config.ElementPower6 });
        baseProp.Add(new() { Key = (int)EAttributeType.SpecialDamageChange, Value = config.SpecialDamageChange });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthFastClimbCost, Value = config.StrengthFastClimbCost });
        baseProp.Add(new() { Key = (int)EAttributeType.ElementPropertyType, Value = config.ElementPropertyType });
        baseProp.Add(new() { Key = (int)EAttributeType.WeakTime, Value = config.WeakTime });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDefRate, Value = config.IgnoreDefRate });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistancePhys, Value = config.IgnoreDamageResistancePhys });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement1, Value = config.IgnoreDamageResistanceElement1 });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement2, Value = config.IgnoreDamageResistanceElement2 });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement3, Value = config.IgnoreDamageResistanceElement3 });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement4, Value = config.IgnoreDamageResistanceElement4 });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement5, Value = config.IgnoreDamageResistanceElement5 });
        baseProp.Add(new() { Key = (int)EAttributeType.IgnoreDamageResistanceElement6, Value = config.IgnoreDamageResistanceElement6 });
        baseProp.Add(new() { Key = (int)EAttributeType.SkillToughRatio, Value = config.SkillToughRatio });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthClimbJump, Value = config.StrengthClimbJump });
        baseProp.Add(new() { Key = (int)EAttributeType.StrengthGliding, Value = config.StrengthGliding });
        baseProp.Add(new() { Key = (int)EAttributeType.Mass, Value = config.Mass });
        baseProp.Add(new() { Key = (int)EAttributeType.BrakingFrictionFactor, Value = config.BrakingFrictionFactor });
        baseProp.Add(new() { Key = (int)EAttributeType.GravityScale, Value = config.GravityScale });
        baseProp.Add(new() { Key = (int)EAttributeType.SpeedRatio, Value = config.SpeedRatio });
        baseProp.Add(new() { Key = (int)EAttributeType.DamageChangePhantom, Value = config.DamageChangePhantom });
        baseProp.Add(new() { Key = (int)EAttributeType.AutoAttackSpeed, Value = config.AutoAttackSpeed });
        baseProp.Add(new() { Key = (int)EAttributeType.CastAttackSpeed, Value = config.CastAttackSpeed });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp1Max, Value = config.StatusBuildUp1Max });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp1, Value = config.StatusBuildUp1 });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp2Max, Value = config.StatusBuildUp2Max });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp2, Value = config.StatusBuildUp2 });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp3Max, Value = config.StatusBuildUp3Max });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp3, Value = config.StatusBuildUp3 });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp4Max, Value = config.StatusBuildUp4Max });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp4, Value = config.StatusBuildUp4 });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp5Max, Value = config.StatusBuildUp5Max });
        baseProp.Add(new() { Key = (int)EAttributeType.StatusBuildUp5, Value = config.StatusBuildUp5 });
        baseProp.Add(new() { Key = (int)EAttributeType.ParalysisTimeMax, Value = config.ParalysisTimeMax });
        baseProp.Add(new() { Key = (int)EAttributeType.ParalysisTime, Value = config.ParalysisTime });
        baseProp.Add(new() { Key = (int)EAttributeType.ParalysisTimeRecover, Value = config.ParalysisTimeRecover });

        return baseProp;
    }
}
