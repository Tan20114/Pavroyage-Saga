public class SkillAction : IBattleAction
{
    readonly Character user;
    readonly Character target;
    readonly SkillData skill;
    public bool EndsTurn => true;
    public string AnimationState => "";

    public SkillAction(Character user, Character target, SkillData skill)
    {
        this.user = user;
        this.target = target;
        this.skill = skill;
    }


    public bool CanExecute()
    {
        if (user == null || target == null || skill == null) return false;

        if (user.IsDead || target.IsDead) return false;

        if (user.SkillPoints < skill.skillPointCost) return false;

        return true;
    }

    public void Execute()
    {
        user.skillExecutor.Execute(skill, target);
    }
}