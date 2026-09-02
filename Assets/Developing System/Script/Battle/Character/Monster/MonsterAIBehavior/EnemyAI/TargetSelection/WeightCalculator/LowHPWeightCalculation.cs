using UnityEngine;

public class LowHPWeightCalculation : ITargetCalculator
{
    public float CalculateWeight(Character self, Character candidate)
    {
        if (candidate == null || candidate.IsDead)
            return 0f;

        if (candidate.derivedStats.GetDerived(DerivedType.MaxHp) <= 0)
            return 0f;

        float hpRatio = (float)candidate.CurrentHP / candidate.derivedStats.GetDerived(DerivedType.MaxHp);

        return 1f - hpRatio;
    }
}
