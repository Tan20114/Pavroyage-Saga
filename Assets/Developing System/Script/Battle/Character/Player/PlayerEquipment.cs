using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializedDictionary("Equipment Type", "Equipment")]
    public SerializedDictionary<EquipmentType, Equipment> equippedItems;

    public delegate void OnEquipmentChange(Equipment current, Equipment previous, bool equipped);
    public event OnEquipmentChange onEquipmentChange;

    public void Equip(Equipment equipment)
    {
        if (equipment == null) return;

        equippedItems.TryGetValue(equipment.type, out Equipment previous);

        equippedItems[equipment.type] = equipment;

        onEquipmentChange?.Invoke(equipment, previous, true);
    }

    public void Unequip(EquipmentType equipmentType)
    {
        if (!equippedItems.TryGetValue( equipmentType, out Equipment equipment)) return;

        equippedItems.Remove(equipmentType);

        onEquipmentChange?.Invoke(null, equipment, false);
    }

    public Equipment GetEquipment(EquipmentType equipmentType)
    {
        equippedItems.TryGetValue(
            equipmentType,
            out Equipment equipment
        );

        return equipment;
    }

    public bool IsEquipped(EquipmentType equipmentType)
    {
        return equippedItems.ContainsKey(equipmentType);
    }

    public void CalculateLastStat(CharacterStats baseStats, CharacterDerivedStat derivedStat)
    {
        baseStats.ResetModifier();
        derivedStat.ResetModifier();

        foreach (Equipment equipment in equippedItems.Values)
        {
            if (equipment == null)
                continue;

            foreach (var modifier in equipment.stats.statBonuses)
            {
                StatType statType = modifier.Key;
                int statValue = modifier.Value;

                baseStats.ModifyStat(statType,statValue);
            }

            foreach (var modifier in equipment.stats.derivedBonuses)
            {
                DerivedType derivedType = modifier.Key;
                float derivedValue = modifier.Value;

                derivedStat.ModifyDerived(derivedType, derivedValue);
            }
        }

        baseStats.CalculateStats();
        derivedStat.CalculatePlayerStat();
    }
}