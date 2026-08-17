using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
public class Monster : Character
{
    private void Start()
    {
        Debug.Log($"STR: {baseStats.GetStat(StatType.STR)}");

        derivedStats.CalculateStat();

        Debug.Log(
            $"Damage: {derivedStats.GetDerived(DerivedType.PhysicalDamage)}"
        );
    }
}