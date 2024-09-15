using Protocol;

namespace GameServer.Models;
internal class SkillModel
{
    public List<ArrayIntInt> SkillsList { get; } = [];
    public List<ArraySkillNode> SkillNodesList { get; } = [];

    public ArrayIntInt Addskill(int skillId ,int lv)
    {
        ArrayIntInt skill = new()
        {
            Key = skillId,
            Value = lv
        };
        SkillsList.Add(skill);
        return skill;
    }

    public void ClearSkills()
    {
        SkillsList.Clear();
        SkillNodesList.Clear();
    }

    public ArraySkillNode AddSkillNode(int skillNodeId, bool isActive, int skillId)
    {
        ArraySkillNode skillNode = new()
        {
            SkillNodeId = skillNodeId,
            IsActive = isActive,
            SkillId = skillId
        };
        SkillNodesList.Add(skillNode);
        return skillNode;
    }



}
