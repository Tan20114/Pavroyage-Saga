using UnityEngine;

public class VictoryState : IBattleState
{
    readonly BattleUI battleUI;

    public VictoryState(BattleUI battleUI)
    {
        this.battleUI = battleUI;
    }

    public void Enter()
    {
        Debug.Log("Victory!");
        GlobalManager.Instance.QuestManager.CompleteQuest();
        battleUI.ShowVictoryPanel();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}