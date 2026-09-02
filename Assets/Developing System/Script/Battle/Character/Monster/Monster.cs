using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
public class Monster : Character
{
    [HideInInspector] public MonsterAI ai;

    protected override void Awake()
    {
        base.Awake();
        ai = new MonsterAI(this);
    }

    protected override void OnEnable()
    {
        derivedStats.CalculateStat();

        base.OnEnable();
    }
}