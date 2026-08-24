public interface ISkillStrategy
{
    void Execute(Character user, Character target, SkillData skill);
}