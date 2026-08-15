using UnityEngine;

public class Monster : Character
{
    private void Start()
    {
        derivedStats.CalculateStat();
    }
}