using UnityEngine;

public class DefeatState : IBattleState
{
    readonly BattleFlow battleFlow;

    public DefeatState(BattleFlow battleFlow)
    {
        this.battleFlow = battleFlow;
    }

    public void Enter()
    {
        Debug.Log("Defeat!");

        GlobalManager.Instance.SetBattleResult(BattleResult.Defeat);

        battleFlow.GoToMainMenu();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}