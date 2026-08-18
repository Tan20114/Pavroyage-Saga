using UnityEngine;

public class DMGCalculatorSelection : MonoBehaviour
{
    public static IDamageCalculator Create(DamageCategory category)
    {
        return category switch
        {
            DamageCategory.Physical => new PhysicalDamageCalculation(),
            DamageCategory.Magic => new MagicDamageCalculation(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(category))
        };
    }
}
