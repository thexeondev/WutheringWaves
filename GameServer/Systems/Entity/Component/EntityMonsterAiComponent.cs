using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityMonsterAiComponent : EntityComponentBase
{
    public override EntityComponentType Type => EntityComponentType.MonsterAi;

    public int AiTeamInitId { get; set; }

    public override EntityComponentPb Pb => new()
    {
        MonsterAiComponentPb = new MonsterAiComponentPb
        {
            AiTeamInitId = AiTeamInitId,
        }
    };
}
