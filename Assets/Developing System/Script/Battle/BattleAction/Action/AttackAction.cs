using UnityEngine;

public class AttackAction : IBattleAction
{
    readonly Character attacker;
    readonly Character target;
    public bool EndsTurn => true;
    public string AnimationState => "Attack";

    public AttackAction(Character attacker, Character defender)
    {
        this.attacker = attacker;
        target = defender;
    }

    public bool CanExecute()
    {
        if (attacker == null || target == null) return false;

        if (attacker.IsDead || target.IsDead) return false;

        return true;
    }

    public void Execute()
    {
        attacker.SkillPoints++;
        attacker.Attack(target, DamageCategory.Physical, Element.None);
    }
}