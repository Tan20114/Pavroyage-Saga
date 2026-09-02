using UnityEngine;

public class MaxHPWeightCalculation : ITargetCalculator
{
    public float CalculateWeight(Character self, Character candidate)
    {
        if (candidate == null || candidate.IsDead)
            return 0f;

        if (candidate.derivedStats.GetDerived(DerivedType.MaxHp) <= 0)
            return 0f;

        float minHp = 10f;
        float maxHpRange = 500f;

        return Mathf.InverseLerp(maxHpRange, minHp, candidate.derivedStats.GetDerived(DerivedType.MaxHp));
    }
}
