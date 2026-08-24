public class DamageSkillStrategy : ISkillStrategy
{
    public void Execute(Character user, Character target, SkillData skill)
    {
        IDamageCalculator calculator =
            DMGCalculatorSelection.Create(skill.category);

        DamageContext context = new DamageContext
        {
            attacker = user,
            defender = target,
            category = skill.category,
            element = skill.element
        };

        int damage = calculator.Calculate(
            context,
            skill.damageMultiplier
        );

        target.TakeDamage(damage);
    }
}