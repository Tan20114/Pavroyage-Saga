public interface IBattleAction
{
    bool CanExecute();
    bool EndsTurn { get; }
    void Execute();
}
