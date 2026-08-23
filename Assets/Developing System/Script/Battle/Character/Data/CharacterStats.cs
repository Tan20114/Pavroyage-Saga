using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum StatType
{
    STR,
    DEX,
    CON,
    INT,
    LCK,
    CHA
}

public class CharacterStats : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] CharacterStatData baseStats;

    [Header("Stat")]
    public int level;
    BaseStats modifierStats;
    [SerializeField] BaseStats stats;

    private void Awake()
    {
        modifierStats = new BaseStats();
        stats = new BaseStats();

        CalculateStats();
    }

    public int GetStat(StatType type)
    {
        return type switch
        {
            StatType.STR => stats.STR,
            StatType.DEX => stats.DEX,
            StatType.INT => stats.INT,
            StatType.LCK => stats.LCK,
            StatType.CON => stats.CON,
            StatType.CHA => stats.CHA,
        };
    }

    public void ModifyStat(StatType type, int value)
    {
        switch (type)
        {
            case StatType.STR: modifierStats.STR += value; break;
            case StatType.DEX: modifierStats.DEX += value; break;
            case StatType.CON: modifierStats.CON += value; break;
            case StatType.INT: modifierStats.INT += value; break;
            case StatType.LCK: modifierStats.LCK += value; break;
            case StatType.CHA: modifierStats.CHA += value; break;
        }
    }

    public void CalculateStats()
    {
        stats.STR = baseStats.stats.STR + modifierStats.STR;
        stats.DEX = baseStats.stats.DEX + modifierStats.DEX;
        stats.CON = baseStats.stats.CON + modifierStats.CON;
        stats.INT = baseStats.stats.INT + modifierStats.INT;
        stats.LCK = baseStats.stats.LCK + modifierStats.LCK;
        stats.CHA = baseStats.stats.CHA + modifierStats.CHA;
    }

    public void ResetModifier()
    {
        modifierStats.STR = 0;
        modifierStats.DEX = 0;
        modifierStats.INT = 0;
        modifierStats.LCK = 0;
        modifierStats.CON = 0;
        modifierStats.CHA = 0;
    }
}

[System.Serializable]
public class BaseStats
{
    public int STR = 0;
    public int DEX = 0;
    public int CON = 0;
    public int INT = 0;
    public int LCK = 0;
    public int CHA = 0;
}