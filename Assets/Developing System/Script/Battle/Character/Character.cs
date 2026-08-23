using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour
{
    const int MAX_SKILL_POINT = 10;

    #region Reference
    [HideInInspector] public CharacterDerivedStat derivedStats;
    [HideInInspector] public CharacterStats baseStats;
    [HideInInspector] public ActionExecutor actionExecutor { get; private set; }
    [HideInInspector] public SkillExecutor skillExecutor { get; private set; }
    public event DeadEvent onDead;
    public delegate void DeadEvent(Character character);
    #endregion

    [Header("Description")]
    [SerializeField] protected string characterName;
    [Header("Status")]
    [SerializeField] int currentHP = 1;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        private set
        {
            currentHP = (int)Mathf.Clamp(value, 0, derivedStats.GetDerived(DerivedType.MaxHp));
        }
    }
    [SerializeField] int skillPoints = 0;
    public int SkillPoints
    {
        get
        {
            return skillPoints;
        }

        set
        {
            skillPoints = Mathf.Clamp(value, 0, MAX_SKILL_POINT);
        }
    }

    public bool IsDead
    {
        get => CurrentHP <= 0;
    }
    [Header("Action")]
    [SerializeField] protected List<SkillData> skillList;
    public IReadOnlyList<SkillData> skills => skillList;

    protected virtual void Awake()
    {
        actionExecutor = new ActionExecutor(this);
        skillExecutor = new SkillExecutor(this);

        baseStats = GetComponent<CharacterStats>();
        derivedStats = GetComponent<CharacterDerivedStat>();
    }

    protected virtual void OnEnable()
    {
        CurrentHP = (int)derivedStats.GetDerived(DerivedType.MaxHp);
    }

    public void Attack(Character target, DamageCategory category, Element element)
    {
        IDamageCalculator calculator = DMGCalculatorSelection.Create(category);

        DamageContext context = new DamageContext
        {
            attacker = this,
            defender = target,
            category = category,
            element = element
        };

        int damage = calculator.Calculate(context);

        target.TakeDamage(damage);

        Debug.Log($"{this.name} attacked {target.name} for {damage} damage in {category} {element}");
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        Debug.Log(gameObject.name + $" take {damage} damage");
        CurrentHP -= damage;

        if (IsDead)
        {
            onDead?.Invoke(this);
            Debug.Log(gameObject.name + " Dead");
        }
    }

    public void ExecuteAction(IBattleAction action)
    {
        actionExecutor.Execute(action);
    }
}