using UnityEngine;

public enum DerivedType
{
    PhysicalDamage,
    MaxHp,
    CriticalChance,
    CriticalDamage,
    BlockDamageReduction,
    DodgeChance,
    Speed,
    DiscoveryRate,
    MagicDamage,
    ArmorPierce,
    AggroRateReduction,
    MagicResistance,
    PhysicalResistance,
    DebuffResistance
}

[RequireComponent(typeof(CharacterStats))]
public class CharacterDerivedStat : MonoBehaviour
{
    CharacterStats baseStats => GetComponent<CharacterStats>();
    CommonDerivedStats derivedStats = new CommonDerivedStats();
    CommonDerivedStats modifierStat = new CommonDerivedStats();

    CommonDerivedStats finalDerivedStat = new CommonDerivedStats();

    public float GetDerived(DerivedType type)
    {
        return type switch
        {
            DerivedType.PhysicalDamage => finalDerivedStat.physicalDamage,
            DerivedType.MaxHp => finalDerivedStat.maxHP,
            DerivedType.CriticalChance => finalDerivedStat.criticalChance,
            DerivedType.CriticalDamage => (finalDerivedStat.criticalDamage + 150) / 100,
            DerivedType.ArmorPierce => finalDerivedStat.armourPierce / 100,
            DerivedType.BlockDamageReduction => finalDerivedStat.blockDamageReduction / 100,
            DerivedType.DodgeChance => finalDerivedStat.dodgeChance / 100,
            DerivedType.Speed => finalDerivedStat.speed,
            DerivedType.DiscoveryRate => finalDerivedStat.discoveryRate / 100,
            DerivedType.MagicDamage => finalDerivedStat.magicDamage,
            DerivedType.AggroRateReduction => finalDerivedStat.aggroRateReduction / 100,
            DerivedType.MagicResistance => finalDerivedStat.magicResistance / 100,
            DerivedType.PhysicalResistance => finalDerivedStat.physicalResistance / 100,
            DerivedType.DebuffResistance => finalDerivedStat.debuffResistance / 100,
            _ => 0f
        };
    }

    public void ModifyDerived(DerivedType type, float value)
    {
        switch (type)
        {
            case DerivedType.PhysicalDamage: modifierStat.physicalDamage += value; break;
            case DerivedType.MaxHp: modifierStat.maxHP += value; break;
            case DerivedType.ArmorPierce: modifierStat.armourPierce += value; break;
            case DerivedType.CriticalChance: modifierStat.criticalChance += value; break;
            case DerivedType.CriticalDamage: modifierStat.criticalDamage += value; break;
            case DerivedType.BlockDamageReduction: modifierStat.blockDamageReduction += value; break;
            case DerivedType.DodgeChance: modifierStat.dodgeChance += value; break;
            case DerivedType.Speed: modifierStat.speed += (int)value; break;
            case DerivedType.DiscoveryRate: modifierStat.discoveryRate += value; break;
            case DerivedType.MagicDamage: modifierStat.magicDamage += value; break;
            case DerivedType.AggroRateReduction: modifierStat.aggroRateReduction += value; break;
            case DerivedType.MagicResistance: modifierStat.magicResistance += value; break;
            case DerivedType.PhysicalResistance: modifierStat.physicalResistance += value; break;
            case DerivedType.DebuffResistance: modifierStat.debuffResistance += value; break;
        }
    }

    public void ResetModifier()
    {
        modifierStat.physicalDamage = 0;
        modifierStat.maxHP = 0;
        modifierStat.criticalChance = 0;
        modifierStat.criticalDamage = 0;
        modifierStat.blockDamageReduction = 0;
        modifierStat.dodgeChance = 0;
        modifierStat.speed = 0;
        modifierStat.discoveryRate = 0;
        modifierStat.magicDamage = 0;
        modifierStat.aggroRateReduction = 0;
        modifierStat.magicResistance = 0;
        modifierStat.physicalResistance = 0;
        modifierStat.debuffResistance = 0;
        modifierStat.armourPierce = 0;
    }

    #region Stat Calculations
    public void CalculatePlayerStat()
    {
        CalculatePlayerOffensiveStats();
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
        finalDerivedStat.physicalDamage = derivedStats.physicalDamage + modifierStat.physicalDamage;
    }

    void CalculateMagicDamage()
    {
        int intelligence = baseStats.GetStat(StatType.INT);
        derivedStats.magicDamage = intelligence;
        finalDerivedStat.magicDamage = derivedStats.magicDamage + modifierStat.magicDamage;
    }

    void CalculateArmorPierce()
    {
        finalDerivedStat.armourPierce = modifierStat.armourPierce;
    }    

    void CalculateCriticalChance()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int dexterity = baseStats.GetStat(StatType.DEX);
        int luck = baseStats.GetStat(StatType.LCK);
        int intelligence = baseStats.GetStat(StatType.INT);
        derivedStats.criticalChance = (0.12f * strength) + (0.15f * dexterity) + (0.25f * luck) + (0.1f * intelligence);
        finalDerivedStat.criticalChance = derivedStats.criticalChance + modifierStat.criticalChance;
    }

    void CalculateCriticalDamage()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int dexterity = baseStats.GetStat(StatType.DEX);
        int intelligence = baseStats.GetStat(StatType.INT);
        int luck = baseStats.GetStat(StatType.LCK);
        derivedStats.criticalDamage = (0.2f * strength) + (0.15f * dexterity) + (0.1f * intelligence) + (0.35f * luck);
        finalDerivedStat.criticalDamage = derivedStats.criticalDamage + modifierStat.criticalDamage;
    }

    void CalculateSpeed()
    {
        int dexterity = baseStats.GetStat(StatType.DEX);
        derivedStats.speed = dexterity;
        finalDerivedStat.speed = derivedStats.speed + modifierStat.speed;
    }

    void CalculateOffensiveStats()
    {
        CalculatePhysicalDamage();
        CalculateMagicDamage();
        CalculateCriticalChance();
        CalculateCriticalDamage();
        CalculateSpeed();
    }

    void CalculatePlayerOffensiveStats()
    {
        CalculateOffensiveStats();
        CalculateArmorPierce();
    }
    #endregion

    #region Defensive Stats
    #region Common
    void CalculateMaxHP()
    {
        int strength = baseStats.GetStat(StatType.STR);
        int constitution = baseStats.GetStat(StatType.CON);
        derivedStats.maxHP = strength + (2 * constitution);
        finalDerivedStat.maxHP = derivedStats.maxHP + modifierStat.maxHP;
    }

    void CalculatePhysicalResistance()
    {
        int constitution = baseStats.GetStat(StatType.CON);
        derivedStats.physicalResistance = 0.25f * constitution;
        finalDerivedStat.physicalResistance = derivedStats.physicalResistance + modifierStat.physicalResistance;
    }

    void CalculateMagicResistance()
    {
        int intelligence = baseStats.GetStat(StatType.INT);
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.magicResistance = (0.25f * intelligence) + (0.25f * charisma);
        finalDerivedStat.magicResistance = derivedStats.magicResistance + modifierStat.magicResistance;
    }

    void CalculateDodgeChance()
    {
        int dexterity = baseStats.GetStat(StatType.DEX);
        derivedStats.dodgeChance = (0.125f * dexterity);
        finalDerivedStat.dodgeChance = derivedStats.dodgeChance + modifierStat.dodgeChance;
    }

    void CalculateDebuffResistance()
    {
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.debuffResistance = 0.1f * charisma;
        finalDerivedStat.debuffResistance = derivedStats.debuffResistance + modifierStat.debuffResistance;
    }
    #endregion

    #region Player Only
    void CalculateBlock()
    {
        int constitution = baseStats.GetStat(StatType.CON);
        derivedStats.blockDamageReduction = (0.25f * constitution);
        finalDerivedStat.blockDamageReduction = derivedStats.blockDamageReduction + modifierStat.blockDamageReduction;
    }

    void CalculateAggroRateReduction()
    {
        int charisma = baseStats.GetStat(StatType.CHA);
        derivedStats.aggroRateReduction = (0.5f * charisma);
        finalDerivedStat.aggroRateReduction = derivedStats.aggroRateReduction + modifierStat.aggroRateReduction;
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
        finalDerivedStat.discoveryRate = derivedStats.discoveryRate + modifierStat.discoveryRate;
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

    #region Player Only
    public float armourPierce;
    #endregion
    #endregion

    #region Other Stats (Player Only)
    [Header("Other Stats")]
    public float discoveryRate;
    #endregion
}