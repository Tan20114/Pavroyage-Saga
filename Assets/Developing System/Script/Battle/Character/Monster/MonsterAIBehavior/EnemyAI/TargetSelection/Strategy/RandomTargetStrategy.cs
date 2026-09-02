using System.Collections.Generic;
using UnityEngine;

public class RandomTargetStrategy : ITargetSelectionStrategy
{
    public Character SelectTarget(Character self, IReadOnlyList<Character> candidates)
    {
        if (candidates == null || candidates.Count == 0)
        {
            return null;
        }

        return candidates[Random.Range(0, candidates.Count)];
    }
}