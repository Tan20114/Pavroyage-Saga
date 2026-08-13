using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Stat")]
    public int level;
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] BaseStats baseStats;

    #region Base Stat Getters
    public int Strength { get { return baseStats.STR + CalculateModifier(StatType.STR); } }
    public int Dexterity { get { return baseStats.DEX + CalculateModifier(StatType.DEX); } }
    public int Constitution { get { return baseStats.CON + CalculateModifier(StatType.CON); } }
    public int Intelligence { get { return baseStats.INT + CalculateModifier(StatType.INT); } }
    public int Luck { get { return baseStats.LCK + CalculateModifier(StatType.LCK); } }
    public int Charisma { get { return baseStats.CHA + CalculateModifier(StatType.CHA); } }
    #endregion

    #region Base Stat Modifiers Methods
    int CalculateModifier(StatType statType) => playerEquipment.statModifiers.Values
                                                .Where(equiped => equiped != null)
                                                .SelectMany(equiped => equiped.equipmentStat.statBonuses
                                                .Where(stat => stat.Key == statType)
                                                .Select(stat => stat.Value))
                                                .Sum();
    #endregion

    private void Awake()
    {
        playerEquipment = GetComponent<PlayerEquipment>();
    }
}

[System.Serializable]
public class BaseStats
{
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int LCK;
    public int CHA;
}