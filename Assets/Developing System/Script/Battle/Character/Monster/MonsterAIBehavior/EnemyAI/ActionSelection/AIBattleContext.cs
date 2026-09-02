using System.Collections.Generic;

public enum AIActionType
{
    None,
    Attack,
    Skill
}

public class AIBattleContext
{
    public Character Self { get; }

    public IReadOnlyList<Character> Allies { get; }

    public IReadOnlyList<Character> Enemies { get; }

    public AIBattleContext(Character self, IReadOnlyList<Character> allies, IReadOnlyList<Character> enemies)
    {
        Self = self;
        Allies = allies;
        Enemies = enemies;
    }
}
