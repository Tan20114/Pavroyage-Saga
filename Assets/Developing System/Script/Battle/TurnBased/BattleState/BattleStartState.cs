using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : IBattleState
{
    readonly TurnManager turnManager;
    readonly List<Character> characterList;

    Character nextTurnOwner;

    public BattleStartState(TurnManager tm, List<Character> characters)
    {
        turnManager = tm;
        characterList = characters;
    }

    public void Enter()
    {
        //Debug.Log("Start Battle");

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