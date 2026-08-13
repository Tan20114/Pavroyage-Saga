using UnityEngine;

public class Monster : Character
{
    private void Start()
    {
        commonDerivedStats.CalculateOffensiveStats();
        commonDerivedStats.CalculateDefensiveStats();
    }
}