using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("role/roleinfo.json")]
public class RoleInfoConfig : IConfig
{
    public ConfigType Type => ConfigType.RoleInfo;

    public int Identifier => Id;

    public int Id { get; set; }
    public int QualityId { get; set; }
    public int RoleType { get; set; }
    public bool IsTrial { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public int ParentId { get; set; }
    public int Priority { get; set; }
    public int PropertyId { get; set; }
    public List<int> ShowProperty { get; set; } = [];
    public int ElementId { get; set; }
    public string RoleHeadIconLarge { get; set; } = string.Empty;
    public string RoleHeadIconBig { get; set; } = string.Empty;
    public string Card { get; set; } = string.Empty;
    public string RoleHeadIcon { get; set; } = string.Empty;
    public string FormationRoleCard { get; set; } = string.Empty;
    public string RoleStand { get; set; } = string.Empty;
    public int MeshId { get; set; }
    public int UiMeshId { get; set; }
    public string RoleBody { get; set; } = string.Empty;
    public int BreachModel { get; set; }
    public int SpecialEnergyBarId { get; set; }
    public string CameraConfig { get; set; } = string.Empty;
    public int CameraFloatHeight { get; set; }
    public int EntityProperty { get; set; }
    public int MaxLevel { get; set; }
    public int LevelConsumeId { get; set; }
    public int BreachId { get; set; }
    public int SkillId { get; set; }
    public int SkillTreeGroupId { get; set; }
    public int ResonanceId { get; set; }
    public int ResonantChainGroupId { get; set; }
    public bool IsShow { get; set; }
    public int InitWeaponItemId { get; set; }
    public int WeaponType { get; set; }
    public string SkillDAPath { get; set; } = string.Empty;
    public string SkillLockDAPath { get; set; } = string.Empty;
    public string UiScenePerformanceABP { get; set; } = string.Empty;
    public int LockOnDefaultId { get; set; }
    public int LockOnLookOnId { get; set; }
    public string SkillEffectDA { get; set; } = string.Empty;
    public string FootStepState { get; set; } = string.Empty;
    public int PartyId { get; set; }
    public string AttributesDescription { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int ItemQualityId { get; set; }
    public string ObtainedShowDescription { get; set; } = string.Empty;
    public int NumLimit { get; set; }
    public bool ShowInBag { get; set; }
    public List<float> WeaponScale { get; set; } = [];
    public bool Intervene { get; set; }
    public string CharacterVoice { get; set; } = string.Empty;
    public int TrialRole { get; set; }
    public bool IsAim { get; set; }
    public int RoleGuide { get; set; }
    public int RedDotDisableRule { get; set; }
}
