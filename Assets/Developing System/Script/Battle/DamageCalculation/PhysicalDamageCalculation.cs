using UnityEngine;

public class PhysicalDamageCalculation : IDamageCalculator
{
    public int Calculate(DamageContext context)
    {
        CharacterDerivedStat attackerDerivedStat = context.attacker.derivedStats;
        CharacterDerivedStat defenderDerivedStat = context.defender.derivedStats;

        float criticalMultiplier = CriticalCalcuations(attackerDerivedStat) ? attackerDerivedStat.GetDerived(DerivedType.CriticalDamage) : 1;

        return (int)
               (attackerDerivedStat.GetDerived(DerivedType.PhysicalDamage)
               * criticalMultiplier
               * ResistanceCalcuations(attackerDerivedStat,defenderDerivedStat)
               * BlockCalculation(context.defender));
    }

    public int Calculate(DamageContext context, float skillMultiplier)
    {
        CharacterDerivedStat attackerDerivedStat = context.attacker.derivedStats;
        CharacterDerivedStat defenderDerivedStat = context.defender.derivedStats;

        float criticalMultiplier = CriticalCalcuations(attackerDerivedStat) ? attackerDerivedStat.GetDerived(DerivedType.CriticalDamage) : 1;

        return (int)
               (attackerDerivedStat.GetDerived(DerivedType.PhysicalDamage)
               * skillMultiplier
               * criticalMultiplier
               * ResistanceCalcuations(attackerDerivedStat, defenderDerivedStat)
               * BlockCalculation(context.defender));
    }

    public bool CriticalCalcuations(CharacterDerivedStat attackerDerivedStat)
    {
        float ranVal = Random.Range(0, 100);

        return ranVal < attackerDerivedStat.GetDerived(DerivedType.CriticalChance);
    }

    public float ResistanceCalcuations(CharacterDerivedStat attackerStat, CharacterDerivedStat defenderStat)
    {
        return 1 - (defenderStat.GetDerived(DerivedType.PhysicalResistance) - attackerStat.GetDerived(DerivedType.ArmorPierce));
    }

    public float BlockCalculation(Character defender)
    {
        if (defender == null) return 0;

        if (defender is Player player)
        {
            return player.IsBlocking ? 1 - defender.derivedStats.GetDerived(DerivedType.BlockDamageReduction) : 1;
        }
        else
            return 1;
    }
}