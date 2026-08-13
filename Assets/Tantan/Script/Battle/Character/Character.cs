using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent (typeof(CharacterDerivedStat))]
public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    public CharacterStats characterStats;
    public CharacterDerivedStat commonDerivedStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        commonDerivedStats = GetComponent<CharacterDerivedStat>();
    }

    private void Start()
    {
        Debug.Log($"Character Name: {characterName}");
        Debug.Log($"Character Strength: {characterStats.Strength}\n" +
                  $"Character Dexterity: {characterStats.Dexterity}\n" +
                  $"Character Constitution: {characterStats.Constitution}\n" +
                  $"Character Intelligence: {characterStats.Intelligence}\n" +
                  $"Character Luck: {characterStats.Luck}\n" +
                  $"Character Charisma: {characterStats.Charisma}\n");
    }
}
