using System.Collections.Generic;
using UnityEngine;

public class CandidateGetter
{
    public IReadOnlyList<Character> GetCandidates(AIBattleContext context, SkillData skill)
    {
        return skill.skillType switch
        {
            SkillType.Heal => context.Allies,
            SkillType.Buff => context.Allies,
            SkillType.Damage => context.Enemies,
            SkillType.Debuff => context.Enemies,
            _ => new List<Character>()
        };
    }
}
