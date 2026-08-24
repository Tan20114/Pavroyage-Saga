using UnityEngine;

public enum SkillType
{
    Damage,
    Buff,
    Debuff,
    Heal
}

[CreateAssetMenu(menuName = "Skill/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Basic Info")]
    public string skillName;
    public SkillType skillType;

    [Header("Damage")]
    public DamageCategory category;
    public Element element;
    public float damageMultiplier;

    [Header("Cost")]
    public int skillPointCost;
}