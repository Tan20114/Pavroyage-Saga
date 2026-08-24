using System.Collections.Generic;
using UnityEngine;

public class SmartTargetSelectionStrategy : ITargetSelectionStrategy
{
    private readonly List<ITargetDecisionMaker> evaluators;

    public SmartTargetSelectionStrategy(List<ITargetDecisionMaker> evaluators)
    {
        this.evaluators = evaluators;
    }

    public Character SelectTarget(Character self, List<Character> candidates)
    {
        // เราจะ implement ต่อใน Step ถัดไป
        return null;
    }
}