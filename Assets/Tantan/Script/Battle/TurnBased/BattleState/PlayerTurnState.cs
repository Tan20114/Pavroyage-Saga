using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : IBattleState
{
    readonly Player player;
    readonly IReadOnlyList<Character> allPlayer;
    readonly IReadOnlyList<Character> allMonster;

    public PlayerTurnState(Player player, IReadOnlyList<Character> playerCharacter, IReadOnlyList<Character> monsterCharacter)
    {
        this.player = player;
        allMonster = monsterCharacter;
        allPlayer = playerCharacter;
    }

    public void Enter()
    {
        Debug.Log($"{player} Player Turn");

        allMonster[3].TakeDamage(9999);

        BattleManager.Instance.NextTurn();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}