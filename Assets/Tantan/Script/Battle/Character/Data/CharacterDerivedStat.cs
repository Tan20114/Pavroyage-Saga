using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterDerivedStat : MonoBehaviour
{
    CharacterStats baseStats;
    public CommonDerivedStats derivedStats;

    private void Awake()
    {
        baseStats = GetComponent<CharacterStats>();
        derivedStats = new CommonDerivedStats();
    }

    #region Stat Calculations
    public void CalculatePlayerStat()
    {
        CalculateOffensiveStats();
        CalculatePlayerDefensiveStats();
        CalculateOtherStats();
    }

    public void CalculateStat()
    {
        CalculateOffensiveStats();
        CalculateDefensiveStats();
    }

    #region Offensive Stats
    void CalculatePhysicalDamage()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int dexterity = baseStats.GetStat(StatType.DEX);
        int intelligence = baseStats.GetStat(StatType.INT);
        derivedStats.physicalDamage = strength + (0.75f * dexterity) + (0.5f * intelligence);
    }

    void CalculateMagicDamage()
    {
        int intelligence = baseStats.GetStat(StatType.INT);
        derivedStats.magicDamage = intelligence;
    }

    void CalculateCriticalChance()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int dexterity = baseStats.GetStat(StatType.DEX);
        int luck = baseStats.GetStat(StatType.LCK);
        int intelligence = baseStats.GetStat(StatType.INT);
        derivedStats.criticalChance = (0.12f * strength) + (0.15f * dexterity) + (0.25f * luck) + (0.1f * intelligence);
    }

    void CalculateCriticalDamage()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int dexterity = baseStats.GetStat(StatType.DEX);
        int intelligence = baseStats.GetStat(StatType.INT);
        int luck = baseStats.GetStat(StatType.LCK);
        derivedStats.criticalDamage = (0.2f * strength) + (0.15f * dexterity) + (0.1f * intelligence) + (0.35f * luck);
    }

    void CalculateSpeed()
    {
        int dexterity = baseStats.GetStat(StatType.DEX);
        derivedStats.speed = dexterity;
    }

    void CalculateOffensiveStats()
    {
        CalculatePhysicalDamage();
        CalculateMagicDamage();
        CalculateCriticalChance();
        CalculateCriticalDamage();
        CalculateSpeed();
    }
    #endregion

    #region Defensive Stats
    #region Common
    void CalculateMaxHP()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int constitution = baseStats.GetStat(StatType.CON);

        derivedStats.maxHP = strength + (2 * constitution);
    }

    void CalculatePhysicalResistance()
    {
        int constitution = baseStats.GetStat(StatType.CON);
        derivedStats.physicalResistance = 0.25f * constitution;
    }

    void CalculateMagicResistance()
    {
        int intelligence = baseStats.GetStat(StatType.INT);
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.magicResistance = (0.25f * intelligence) + (0.25f * charisma);
    }

    void CalculateDodgeChance()
    {
        int dexterity = baseStats.GetStat(StatType.DEX);
        derivedStats.dodgeChance = (0.125f * dexterity);
    }

    void CalculateDebuffResistance()
    {
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.debuffResistance = 0.1f * charisma;
    }
    #endregion

    #region Player Only
    void CalculateBlock()
    {
        int constitution = baseStats.GetStat(StatType.CON);
        derivedStats.blockDamageReduction = (0.25f * constitution);
    }

    void CalculateAggroRateReduction()
    {
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.aggroRateReduction = (0.5f * charisma);
    }
    #endregion

    void CalculateDefensiveStats()
    {
        CalculateMaxHP();
        CalculatePhysicalResistance();
        CalculateMagicResistance();
        CalculateDodgeChance();
        CalculateDebuffResistance();
    }

    void CalculatePlayerDefensiveStats()
    {
        CalculateDefensiveStats();
        CalculateBlock();
        CalculateAggroRateReduction();
    }
    #endregion

    #region Other Stats
    void CalculateDiscoveryRate()
    {
        int luck = baseStats.GetStat(StatType.LCK);
        derivedStats.discoveryRate = (0.25f * luck);
    }

    void CalculateOtherStats()
    {
        CalculateDiscoveryRate();
    }
    #endregion
    #endregion
}

[System.Serializable]
public class CommonDerivedStats
{
    [Header("Defensive Stats")]
    #region Defensive Stats
    public float maxHP;
    public float physicalResistance;
    public float magicResistance;
    public float dodgeChance;
    public float debuffResistance;

    #region Player Only
    public float blockDamageReduction;
    public float aggroRateReduction;
    #endregion
    #endregion

    [Header("Offensive Stats")]
    #region Offensive Stats
    public float physicalDamage;
    public float magicDamage;
    public float criticalChance;
    public float criticalDamage;
    public int speed;
    #endregion

    #region Other Stats (Player Only)
    [Header("Other Stats")]
    public float discoveryRate;
    #endregion
}