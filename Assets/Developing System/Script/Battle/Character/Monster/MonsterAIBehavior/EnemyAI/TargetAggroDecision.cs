using UnityEngine;

public class TargetAggroDecision : MonoBehaviour
{
    public float Evaluate(Character self,Character target)
    {
        float aggroReduction = target.derivedStats.GetDerived(DerivedType.AggroRateReduction);

        return 1f - aggroReduction;
    }
}
