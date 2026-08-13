using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterDerivedStat : MonoBehaviour
{
    CharacterStats baseStats;
    public CommonDerivedStats commonDerivedStats;

    private void Awake()
    {
        baseStats = GetComponent<CharacterStats>();
        commonDerivedStats = new CommonDerivedStats();
    }

    #region Stat Calculations
    #region Offensive Stats
    void CalculatePhysicalDamage()
    {
        int strength = baseStats.Strength;
        int dexterity = baseStats.Dexterity;
        int intelligence = baseStats.Intelligence;
        commonDerivedStats.physicalDamage = strength + (0.75f * dexterity) + (0.5f * intelligence);
    }

    void CalculateMagicDamage()
    {
        int intelligence = baseStats.Intelligence;
        commonDerivedStats.magicDamage = intelligence;
    }

    void CalculateCriticalChance()
    {
        int strength = baseStats.Strength;
        int dexterity = baseStats.Dexterity;
        int luck = baseStats.Luck;
        int intelligence = baseStats.Intelligence;
        commonDerivedStats.criticalChance = (0.12f * strength) + (0.15f * dexterity) + (0.25f * luck) + (0.1f * intelligence);
    }

    void CalculateCriticalDamage()
    {
        int strength = baseStats.Strength;
        int dexterity = baseStats.Dexterity;
        int intelligence = baseStats.Intelligence;
        int luck = baseStats.Luck;
        commonDerivedStats.criticalDamage = (0.2f * strength) + (0.15f * dexterity) + (0.1f * intelligence) + (0.35f * luck);
    }

    void CalculateSpeed()
    {
        int dexterity = baseStats.Dexterity;
        commonDerivedStats.speed = dexterity;
    }

    public void CalculateOffensiveStats()
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
        int strength = baseStats.Strength;
        int constitution = baseStats.Constitution;

        commonDerivedStats.maxHP = strength + (2 * constitution);
    }

    void CalculatePhysicalResistance()
    {
        int constitution = baseStats.Constitution;
        commonDerivedStats.physicalResistance = 0.25f * constitution;
    }

    void CalculateMagicResistance()
    {
        int intelligence = baseStats.Intelligence;
        int charisma = baseStats.Charisma;
        commonDerivedStats.magicResistance = (0.25f * intelligence) + (0.25f * charisma);
    }

    void CalculateDodgeChance()
    {
        int dexterity = baseStats.Dexterity;
        commonDerivedStats.dodgeChance = (0.125f * dexterity);
    }

    void CalculateDebuffResistance()
    {
        int charisma = baseStats.Charisma;
        commonDerivedStats.debuffResistance = 0.1f * charisma;
    }
    #endregion

    #region Player Only
    void CalculateBlock(PlayerOnlyDerivedStats playerOnlyDerivedStats)
    {
        int constitution = baseStats.Constitution;
        playerOnlyDerivedStats.blockDamageReduction = (0.25f * constitution);
    }

    void CalculateAggroRateReduction(PlayerOnlyDerivedStats playerOnlyDerivedStats)
    {
        int charisma = baseStats.Charisma;
        playerOnlyDerivedStats.aggroRateReduction = (0.5f * charisma);
    }
    #endregion

    public void CalculateDefensiveStats()
    {
        CalculateMaxHP();
        CalculatePhysicalResistance();
        CalculateMagicResistance();
        CalculateDodgeChance();
        CalculateDebuffResistance();
    }

    public void CalculateDefensiveStats(PlayerOnlyDerivedStats playerOnlyDerivedStats)
    {
        CalculateDefensiveStats();
        CalculateBlock(playerOnlyDerivedStats);
        CalculateAggroRateReduction(playerOnlyDerivedStats);
    }
    #endregion

    #region Other Stats
    void CalculateDiscoveryRate(PlayerOnlyDerivedStats playerOnlyDerivedStats)
    {
        int luck = baseStats.Luck;
        playerOnlyDerivedStats.discoveryRate = (0.25f * luck);
    }

    public void CalculateOtherStats(PlayerOnlyDerivedStats playerOnlyDerivedStats)
    {
        CalculateDiscoveryRate(playerOnlyDerivedStats);
    }
    #endregion
    #endregion

    #region Stat Getters
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
    #endregion

    [Header("Offensive Stats")]
    #region Offensive Stats
    public float physicalDamage;
    public float magicDamage;
    public float criticalChance;
    public float criticalDamage;
    public int speed;
    #endregion
}

[System.Serializable]
public class PlayerOnlyDerivedStats
{
    #region Defensive Stats
    public float blockDamageReduction;
    public float aggroRateReduction;
    #endregion

    #region Other Stats
    public float discoveryRate;
    #endregion
}