using System.Collections.Generic;
using UnityEngine;

public class MonsterTurnState : IBattleState
{
    readonly Monster monster;
    readonly IReadOnlyList<Character> allPlayer;
    readonly IReadOnlyList<Character> allMonster;

    public MonsterTurnState(Monster monster, IReadOnlyList<Character> playerCharacter, IReadOnlyList<Character> monsterCharacter)
    {
        this.monster = monster;
        allMonster = monsterCharacter;
        allPlayer = playerCharacter;
    }

    public void Enter()
    {
        Debug.Log($"{monster.name}'s turn");

        monster.ai.InitializeTurn(allPlayer, allMonster);
        IBattleAction action = monster.ai.DecideAction();

        if (action != null)
            monster.ExecuteAction(action);
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}