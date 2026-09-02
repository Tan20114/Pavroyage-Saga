using UnityEngine;

public class HealPriority : ISkillPriorityCalculation
{
    public SkillType SkillType => SkillType.Heal;

    public float Calculate(AIBattleContext context,SkillData skill)
    {
        if (context == null || context.Allies == null || context.Allies.Count == 0)
        {
            return 0f;
        }

        float lowestHpRatio = 1f;

        foreach (Character ally in context.Allies)
        {
            if (ally == null || ally.IsDead)
            {
                continue;
            }

            float hpRatio = (float)ally.CurrentHP / ally.derivedStats.GetDerived(DerivedType.MaxHp);

            if (hpRatio < lowestHpRatio)
            {
                lowestHpRatio = hpRatio;
            }
        }

        float priority = (1f - lowestHpRatio) * 200f;

        return priority;
    }
}