using UnityEngine;

public class DefeatState : IBattleState
{
    readonly BattleUI battleUI;

    public DefeatState(BattleUI battleUI)
    {
        this.battleUI = battleUI;
    }

    public void Enter()
    {
        Debug.Log("Defeat!");
        GlobalManager.Instance.QuestManager.CompleteQuest();
        battleUI.ShowDefeatPanel();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}