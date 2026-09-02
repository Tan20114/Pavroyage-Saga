public class DamagePriority : ISkillPriorityCalculation
{
    public SkillType SkillType => SkillType.Damage;

    public float Calculate(
        AIBattleContext context,
        SkillData skill)
    {
        if (context == null || context.Enemies == null || context.Enemies.Count == 0)
        {
            return 0f;
        }

        float lowestHpRatio = 1f;

        foreach (Character enemy in context.Enemies)
        {
            if (enemy == null || enemy.IsDead) continue;

            float hpRatio =
                (float)enemy.CurrentHP / enemy.derivedStats.GetDerived(DerivedType.MaxHp);

            if (hpRatio < lowestHpRatio)
                lowestHpRatio = hpRatio;
        }

        float lowHpBonus = (1f - lowestHpRatio) * 30f;
        float damageBonus = skill.damageMultiplier * 20f;

        return 50f + lowHpBonus + damageBonus;
    }
}