using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHPBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    Character character;

    void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    void OnEnable()
    {
        if (character == null)
            return;

        character.onHPChanged += UpdateHP;
    }

    void OnDisable()
    {
        if (character == null)
            return;

        character.onHPChanged -= UpdateHP;
    }

    void Start()
    {
        int maxHP = (int)character.derivedStats.GetDerived(DerivedType.MaxHp);

        hpSlider.maxValue = maxHP;
        hpSlider.value = character.CurrentHP;
    }

    void UpdateHP(Character character, int currentHP, int maxHP)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }
}