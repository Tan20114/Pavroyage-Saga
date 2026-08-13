using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
public class Player : Character
{
    public PlayerOnlyDerivedStats playerOnlyDerivedStats;

    protected override void Awake()
    {
        playerOnlyDerivedStats = new PlayerOnlyDerivedStats();
    }

    void Start()
    {
        commonDerivedStats.CalculateOffensiveStats();
        commonDerivedStats.CalculateDefensiveStats(playerOnlyDerivedStats);
        commonDerivedStats.CalculateOtherStats(playerOnlyDerivedStats);
    }
}