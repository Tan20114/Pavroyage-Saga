using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializedDictionary("Equipment Type", "Equipment")]
    public SerializedDictionary<EquipmentType, Equipment> statModifiers;

    public delegate void OnEquipmentChange(Equipment equipment, bool equipped);
    public event OnEquipmentChange onEquipmentChange;

    public void AddStatModifier(Equipment equipment)
    {
        statModifiers[equipment.type] = equipment;
        onEquipmentChange?.Invoke(equipment, true);
    }

    public void RemoveStatModifier(EquipmentType equipmentType)
    {
        if (!statModifiers.TryGetValue(equipmentType, out Equipment equipment))
            return;

        statModifiers.Remove(equipmentType);
        onEquipmentChange?.Invoke(equipment, false);
    }

    public void CalculateLastStat(CharacterStats baseStats, CharacterDerivedStat derivedStat)
    {
        baseStats.ResetModifier();
        derivedStat.ResetModifier();

        foreach (Equipment equipment in statModifiers.Values)
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
    }
}