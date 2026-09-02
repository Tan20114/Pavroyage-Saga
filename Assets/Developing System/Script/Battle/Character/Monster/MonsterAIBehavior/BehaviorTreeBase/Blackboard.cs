using UnityEngine;

public class Blackboard
{
    public Monster Self { get; set; }
    public Character Target { get; set; }
    public AIActionType SelectedAction { get; set; }
    public SkillData SelectedSkill { get; set; }
}
