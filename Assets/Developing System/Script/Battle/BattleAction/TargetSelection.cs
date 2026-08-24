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
        currentIndex++;

        if (currentIndex >= allMonsters.Count)
            currentIndex = 0;
    }

    public void Previous()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = allMonsters.Count - 1;
    }

    public Character GetTarget()
    {
        return allMonsters[currentIndex];
    }
}