public class ActionExecutor
{
    readonly Character owner;

    public ActionExecutor(Character owner)
    {
        this.owner = owner;
    }

    public void Execute(IBattleAction action)
    {
        if (!action.CanExecute())
            return;

        action.Execute();

        if (action.EndsTurn)
            BattleManager.Instance.NextTurn();
    }
}