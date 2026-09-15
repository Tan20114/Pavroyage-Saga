using UnityEngine;

public class SkipAction : IBattleAction
{
    readonly Player user;
    public bool EndsTurn => true;
    public string AnimationState => "";

    public SkipAction(Player user)
    {
        this.user = user;
    }


    public bool CanExecute()
    {
        return user != null && !user.IsDead;
    }

    public void Execute()
    {
        user.SkillPoints += 2;
    }
}
