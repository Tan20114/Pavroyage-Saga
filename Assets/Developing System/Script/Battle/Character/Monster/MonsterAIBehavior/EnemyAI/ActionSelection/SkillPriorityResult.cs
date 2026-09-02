public class SkillPriorityResult
{
    public SkillData Skill { get; }
    public float Priority { get; }

    public SkillPriorityResult(SkillData skill, float priority)
    { 
        Skill = skill;
        Priority = priority;
    }
}