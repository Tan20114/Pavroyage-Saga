using System.Collections.Generic;
using UnityEngine;

public class SkillExecutor
{
    readonly Character user;

    readonly Dictionary<SkillType, ISkillStrategy> strategies;

    public SkillExecutor(Character user)
    {
        this.user = user;

        strategies = new Dictionary<SkillType, ISkillStrategy>
        {
            { SkillType.Damage, new DamageSkillStrategy() },
            { SkillType.Buff, new BuffSkillStrategy() },
            { SkillType.Debuff, new DebuffSkillStrategy() },
            { SkillType.Heal, new HealSkillStrategy() }
        };
    }

    public void Execute(SkillData skill, Character target)
    {
        if (skill == null || target == null) return;

        if (!strategies.TryGetValue(skill.skillType, out ISkillStrategy strategy)) return;

        if (user.SkillPoints < skill.skillPointCost) return;

        strategies[skill.skillType].Execute(user, target, skill);

        Debug.Log($"{user.name} used {skill.name} on {target.name}");

        user.SkillPoints -= skill.skillPointCost;
    }
}