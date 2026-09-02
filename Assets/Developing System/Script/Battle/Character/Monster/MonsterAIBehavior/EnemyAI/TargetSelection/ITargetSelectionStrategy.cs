using System.Collections.Generic;

public interface ITargetSelectionStrategy
{
    Character SelectTarget(Character self, IReadOnlyList<Character> candidates);
}