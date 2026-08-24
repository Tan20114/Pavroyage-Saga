using System.Collections.Generic;

public interface ITargetSelectionStrategy
{
    Character SelectTarget(Character self, List<Character> candidates);
}