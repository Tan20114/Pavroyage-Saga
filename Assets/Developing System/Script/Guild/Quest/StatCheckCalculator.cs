using UnityEngine;

public class StatCheckCalculator
{
    public bool Check( Player player, Quest quest, int act)
    {
        int playerLevel = player.CurrentLevel;

        int questDifficulty = quest.Data.difficulty;

        int finalDifficulty = playerLevel + ((act - 1) * 5) + ((questDifficulty - 1) * 2);

        float playerStat = player.baseStats.GetStat(quest.Data.statCheck);

        float successChance = 50f * (playerStat / finalDifficulty);

        successChance = Mathf.Clamp(successChance, 5f, 95f);

        float roll = Random.Range(0f, 100f);

        return roll <= successChance;
    }
}