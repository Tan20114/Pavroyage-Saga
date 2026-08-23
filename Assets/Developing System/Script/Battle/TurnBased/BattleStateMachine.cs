using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    IBattleState currentState;

    public void ChangeState(IBattleState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}