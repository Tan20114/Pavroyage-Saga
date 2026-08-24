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
        Debug.Log($"{monster.name}'s Turn");

        BattleManager.Instance.NextTurn();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}