using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityMonsterAiComponent : EntityComponentBase
{
    public override EntityComponentType Type => EntityComponentType.MonsterAi;

    public int AiTeamInitId { get; set; }
    public long HatredGroupId { get; set; }
    public int WeaponId { get; set; }

    public override EntityComponentPb Pb => new()
    {
        MonsterAiComponentPb = new MonsterAiComponentPb
        {
            HatredGroupId = HatredGroupId,
            AiTeamInitId = AiTeamInitId,
            WeaponId = WeaponId
        }
    };
}
