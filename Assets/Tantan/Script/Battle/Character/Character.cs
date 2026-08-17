using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    public CharacterStats baseStats;
    public CharacterDerivedStat derivedStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        baseStats = GetComponent<CharacterStats>();
        derivedStats = GetComponent<CharacterDerivedStat>();
    }

    private void Start()
    {
        Debug.Log($"Character Name: {characterName}");
        Debug.Log($"Character Strength: {baseStats.GetStat(StatType.STR)}\n" +
                  $"Character Dexterity: {baseStats.GetStat(StatType.DEX)}\n" +
                  $"Character Constitution: {baseStats.GetStat(StatType.CON)}\n" +
                  $"Character Intelligence: {baseStats.GetStat(StatType.INT)}\n" +
                  $"Character Luck: {baseStats.GetStat(StatType.LCK)}\n" +
                  $"Character Charisma: {baseStats.GetStat(StatType.CHA)}\n");
    }
}
