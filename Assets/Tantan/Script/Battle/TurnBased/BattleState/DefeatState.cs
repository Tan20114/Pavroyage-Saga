using UnityEngine;

public class DefeatState : IBattleState
{
    public void Enter()
    {
        Debug.Log("Defeat!");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}