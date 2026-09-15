public class BlockAction : IBattleAction
{
    readonly Player player;

    public bool EndsTurn => true;
    public string AnimationState => "";

    public BlockAction(Player player)
    {
        this.player = player;
    }

    public bool CanExecute()
    {
        if (player == null)
            return false;

        if (player.IsDead)
            return false;

        return true;
    }

    public void Execute()
    {
        player.SetBlocking(true);
    }
}