using UnityEngine;

public class TargetHPDecision : MonoBehaviour
{
    public float Evaluate(Character self, Character target)
    {
        float maxHp = target.derivedStats.GetDerived(DerivedType.MaxHp);

        if (maxHp <= 0f) return 0f;

        float hpRatio = target.CurrentHP / maxHp;

        return 1f - hpRatio;
    }
}
