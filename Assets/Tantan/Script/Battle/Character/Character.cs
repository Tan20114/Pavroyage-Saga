using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour
{
    #region Reference
    [HideInInspector] public CharacterDerivedStat derivedStats;
    [HideInInspector] public CharacterStats baseStats;
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
    public bool IsDead
    {
        get => CurrentHP <= 0;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
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
}
