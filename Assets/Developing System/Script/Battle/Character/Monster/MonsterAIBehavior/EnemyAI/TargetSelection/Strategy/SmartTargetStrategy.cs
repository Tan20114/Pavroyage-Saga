using System.Collections.Generic;
using UnityEngine;

public class SmartTargetStrategy : ITargetSelectionStrategy
{
    private readonly List<ITargetCalculator> evaluators;

    public SmartTargetStrategy(List<ITargetCalculator> evaluators)
    {
        this.evaluators = evaluators;
    }

    public Character SelectTarget(Character self, IReadOnlyList<Character> candidates)
    {
        if (candidates == null || candidates.Count == 0)
        {
            return null;
        }

        List<float> weights = new List<float>();

        foreach (Character candidate in candidates)
        {
            float score = 0f;

            foreach (ITargetCalculator evaluator in evaluators)
            {
                score += evaluator.CalculateWeight(self, candidate);
            }

            weights.Add(Mathf.Max(0f, score));
        }

        return SelectWeightedRandom(candidates, weights);
    }

    private Character SelectWeightedRandom(IReadOnlyList<Character> candidates, List<float> weights)
    {
        float totalWeight = 0f;

        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        if (totalWeight <= 0f)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }

        float randomValue = Random.Range(0f, totalWeight);

        for (int i = 0; i < candidates.Count; i++)
        {
            randomValue -= weights[i];

            if (randomValue <= 0f)
                return candidates[i];
        }

        return candidates[^1];
    }
}
