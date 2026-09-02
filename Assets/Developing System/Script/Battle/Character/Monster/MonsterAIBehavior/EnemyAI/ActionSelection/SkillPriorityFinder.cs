public class SkillPriorityFinder
{
    public ISkillPriorityCalculation PriorityFind(SkillData skill)
    {
        return skill.skillType switch
        {
            SkillType.Heal => new HealPriority(),
            SkillType.Damage => new DamagePriority(),
            _ => null
        };
    }
}