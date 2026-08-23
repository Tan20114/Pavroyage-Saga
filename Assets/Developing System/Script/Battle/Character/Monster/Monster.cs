using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
public class Monster : Character
{
    protected override void OnEnable()
    {
        derivedStats.CalculateStat();

        base.OnEnable();
    }
}