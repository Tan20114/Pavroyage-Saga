using System.Collections.Generic;

public class TargetSelection
{
    IReadOnlyList<Player> allPlayers;
    IReadOnlyList<Monster> allMonsters;

    int currentIndex;

    public TargetSelection(IReadOnlyList<Player> players, IReadOnlyList<Monster> monsters)
    {
        allPlayers = players;
        allMonsters = monsters;

        currentIndex = 0;
    }

    public void Next()
    {
        if (allMonsters.Count == 0) return;

        int startIndex = currentIndex;

        do
        {
            currentIndex++;

            if (currentIndex >= allMonsters.Count)
                currentIndex = 0;

            if (!allMonsters[currentIndex].IsDead) return;

        } while (currentIndex != startIndex);
    }

    public void Previous()
    {
        if (allMonsters.Count == 0) return;

        int startIndex = currentIndex;

        do
        {
            currentIndex--;

            if (currentIndex < 0)
                currentIndex = allMonsters.Count - 1;

            if (!allMonsters[currentIndex].IsDead) return;

        } while (currentIndex != startIndex);
    }

    public Character GetTarget()
    {
        if (allMonsters.Count == 0) return null;

        if (allMonsters[currentIndex].IsDead)
        {
            Next();

            if (allMonsters[currentIndex].IsDead) return null;
        }

        return allMonsters[currentIndex];
    }
}