using UnityEngine;

public class VictoryState : IBattleState
{
    public void Enter()
    {
        Debug.Log("Victory!");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}