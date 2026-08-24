using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : IBattleState
{
    readonly PlayerTurnController playerTurnController;

    readonly Player player;
    readonly TargetSelectionInputHandler inputHandler;

    public PlayerTurnState(Player player, PlayerTurnController controller, TargetSelectionInputHandler inputHandler)
    {
        this.player = player;
        playerTurnController = controller;
        this.inputHandler = inputHandler;
    }

    public void Enter()
    {
        Debug.Log($"{player} Player Turn");

        playerTurnController.StartTurn(player);
    }

    public void Update()
    {
    }

    public void Exit()
    {
        playerTurnController.EndTurn();
    }
}