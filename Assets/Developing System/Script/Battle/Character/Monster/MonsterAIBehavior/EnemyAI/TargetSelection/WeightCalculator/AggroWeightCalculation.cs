using UnityEngine;

public class AggroWeightCalculation : ITargetCalculator
{
    public float CalculateWeight(Character self, Character candidate)
    {
        if (candidate == null || candidate.IsDead || candidate is Monster)
            return 0f;

        float aggroReduction = candidate.derivedStats.GetDerived(DerivedType.AggroRateReduction);

        return 1f - Mathf.Clamp01(aggroReduction / 100f);
    }
}
