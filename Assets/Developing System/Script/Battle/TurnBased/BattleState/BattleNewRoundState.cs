using System.Collections.Generic;
using UnityEngine;

public class BattleNewRoundState : IBattleState
{
    readonly TurnManager turnManager;
    readonly List<Character> characterList;

    Character nextTurnOwner;

    public BattleNewRoundState(TurnManager tm, List<Character> characters)
    {
        turnManager = tm;
        characterList = characters;
    }

    public void Enter()
    {
        Debug.Log("New Round");

        turnManager.CreateTurnOrder(characterList);

        BattleManager.Instance.NextTurn();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}