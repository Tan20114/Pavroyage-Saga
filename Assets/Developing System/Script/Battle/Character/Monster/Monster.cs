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

    public void Initialize()
    {
        derivedStats.CalculateStat();
        InitialHP();
    }
}