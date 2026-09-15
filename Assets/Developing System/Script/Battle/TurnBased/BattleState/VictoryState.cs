using UnityEngine;

public class VictoryState : IBattleState
{
    readonly BattleFlow battleFlow;

    public VictoryState(BattleFlow battleFlow)
    {
        this.battleFlow = battleFlow;
    }

    public void Enter()
    {
        Debug.Log("Victory!");

        GlobalManager.Instance.SetBattleResult(BattleResult.Victory);

        battleFlow.GoToMainMenu();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}