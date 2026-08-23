using System.Collections.Generic;
using System.Linq;

public class PlayerTurnController
{
    enum PendingActionType
    {
        None,
        Attack,
        Skill
    }

    readonly TargetSelectionInputHandler inputHandler;
    readonly BattleUI battleUI;

    Player player;

    List<IBattleAction> actions = new();

    PendingActionType pendingAction = PendingActionType.None;
    SkillData selectedSkill;

    public PlayerTurnController(BattleUI ui, TargetSelectionInputHandler inputHandler)
    {
        battleUI = ui;
        this.inputHandler = inputHandler;
    }

    public void StartTurn(Player player)
    {
        this.player = player;

        CreateAction();
        inputHandler.onTargetConfirmed += ConfirmTarget;

        battleUI.OpenPlayerPanel();
    }

    public void EndTurn()
    {
        inputHandler.onTargetConfirmed -= ConfirmTarget;

        battleUI.ClosePlayerPanel();
    }

    public void SelectAttack()
    {
        pendingAction = PendingActionType.Attack;
    }

    public void SelectSkill(int skillIndex)
    {
        if (skillIndex > player.skills.Count - 1 || skillIndex < 0) return;

        pendingAction = PendingActionType.Skill;
        selectedSkill = player.skills[skillIndex];
    }

    public void ConfirmTarget(Character target)
    {
        if (target == null) return;

        switch (pendingAction)
        {
            case PendingActionType.Attack:
                player.ExecuteAction(new AttackAction(player, target));
                break;

            case PendingActionType.Skill:
                player.ExecuteAction(new SkillAction(player, target, selectedSkill));
                break;
        }

        pendingAction = PendingActionType.None;
        selectedSkill = null;
    }

    public void SkipTurn()
    {
        player.ExecuteAction(new SkipAction(player));
    }

    void CreateAction()
    {
        actions.Clear();

        actions.Add(new SkipAction(player));
    }

    public bool CanExecute<T>() where T : IBattleAction
    {
        return actions
               .OfType<T>()
               .Any(action => action.CanExecute());
    }

    public bool CanUseSkill(int index)
    {
        int cost = player.skills[index].skillPointCost;

        return player.SkillPoints >= cost;
    }
}