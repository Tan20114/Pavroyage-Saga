public interface IBattleAction
{
    bool CanExecute();
    bool EndsTurn { get; }
    string AnimationState { get; }
    void Execute();
}