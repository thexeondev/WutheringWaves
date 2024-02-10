using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityVisionSkillComponent : EntityComponentBase
{
    public List<VisionSkillInformation> Skills { get; }

    public EntityVisionSkillComponent()
    {
        Skills = [];
    }

    public void SetExploreTool(int toolId)
    {
        Skills.Clear();
        Skills.Add(new VisionSkillInformation
        {
            SkillId = toolId
        });
    }

    public override EntityComponentType Type => EntityComponentType.VisionSkill;

    public override EntityComponentPb Pb
    {
        get
        {
            EntityComponentPb pb = new()
            {
                VisionSkillComponent = new VisionSkillComponentPb()
            };

            pb.VisionSkillComponent.VisionSkillInfos.AddRange(Skills);
            return pb;
        }
    }
}
