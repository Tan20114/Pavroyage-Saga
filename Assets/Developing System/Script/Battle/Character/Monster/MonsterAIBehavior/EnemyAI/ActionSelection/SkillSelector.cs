using System.Collections.Generic;
using System.Linq;

public class SkillSelector
{
    private readonly SkillPriorityFinder priorityFinder;

    public SkillSelector(SkillPriorityFinder priorityFinder)
    {
        this.priorityFinder = priorityFinder;
    }

    public SkillPriorityResult Select(AIBattleContext context, IReadOnlyList<SkillData> skills)
    {
        SkillPriorityResult best = null;

        List<SkillData> availableSkills = skills.Where(skill => skill.skillPointCost <= context.Self.SkillPoints).ToList();

        foreach (SkillData skill in availableSkills)
        {
            ISkillPriorityCalculation calculator = priorityFinder.PriorityFind(skill);

            float priority = calculator.Calculate(context, skill);

            if (best == null || priority > best.Priority)
            {
                best = new SkillPriorityResult(skill, priority);
            }
        }

        return best;
    }

    public SkillPriorityResult Select(AIBattleContext context, IReadOnlyList<SkillData> skills, SkillType skillType)
    {
        SkillPriorityResult best = null;

        List<SkillData> availableSkills = skills
            .Where(skill =>
                   skill.skillPointCost <= context.Self.SkillPoints &&
                   skill.skillType == skillType)
            .ToList();

        foreach (SkillData skill in availableSkills)
        {
            ISkillPriorityCalculation calculator = priorityFinder.PriorityFind(skill);

            float priority = calculator.Calculate(context, skill);

            if (best == null || priority > best.Priority)
            {
                best = new SkillPriorityResult(skill, priority);
            }
        }

        return best;
    }
}