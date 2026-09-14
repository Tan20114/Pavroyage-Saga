using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializedDictionary("Equipment Type", "Equipment")]
    public SerializedDictionary<EquipmentSlot, Equipment> equippedItems;

    public delegate void OnEquipmentChange(Equipment current, Equipment previous, bool equipped);
    public event OnEquipmentChange onEquipmentChange;

    public void LoadData(SerializedDictionary<EquipmentSlot, Equipment> equippedItems)
    {
        this.equippedItems = equippedItems;
    }

    public void Equip(Equipment equipment)
    {
        if (equipment == null) return;

        EquipmentSlot slot;

        if (equipment.type == EquipmentType.Accessory)
        {
            if (!equippedItems.ContainsKey(EquipmentSlot.Accessory1))
                slot = EquipmentSlot.Accessory1;
            else if (!equippedItems.ContainsKey(EquipmentSlot.Accessory2))
                slot = EquipmentSlot.Accessory2;
            else if (!equippedItems.ContainsKey(EquipmentSlot.Accessory3))
                slot = EquipmentSlot.Accessory3;
            else
                return;
        }
        else
        {
            slot = (EquipmentSlot)equipment.type;
        }

        equippedItems.TryGetValue(slot, out Equipment previous);

        equippedItems[slot] = equipment;

        onEquipmentChange?.Invoke(equipment, previous, true);
    }

    public void Unequip(EquipmentType equipmentType)
    {
        if (equipmentType == EquipmentType.Accessory)
        {
            for (int i = 0; i < 3; i++)
            {
                EquipmentSlot slot = EquipmentSlot.Accessory1 + i;

                if (equippedItems.TryGetValue(slot, out Equipment equipped))
                {
                    equippedItems.Remove(slot);
                    onEquipmentChange?.Invoke(null, equipped, false);
                    return;
                }
            }

            return;
        }

        EquipmentSlot equipmentSlot = (EquipmentSlot)equipmentType;

        if (!equippedItems.TryGetValue(equipmentSlot, out Equipment equipment))
            return;

        equippedItems.Remove(equipmentSlot);

        onEquipmentChange?.Invoke(null, equipment, false);
    }

    public Equipment GetEquipment(EquipmentType equipmentType)
    {
        if (equipmentType == EquipmentType.Accessory)
        {
            for (int i = 0; i < 3; i++)
            {
                EquipmentSlot slot = EquipmentSlot.Accessory1 + i;

                if (equippedItems.TryGetValue(slot, out Equipment equipped))
                    return equipped;
            }

            return null;
        }

        EquipmentSlot equipmentSlot = (EquipmentSlot)equipmentType;

        equippedItems.TryGetValue(
            equipmentSlot,
            out Equipment equipment
        );

        return equipment;
    }

    public bool IsEquipped(EquipmentType equipmentType)
    {
        if (equipmentType == EquipmentType.Accessory)
        {
            return equippedItems.ContainsKey(EquipmentSlot.Accessory1)
                || equippedItems.ContainsKey(EquipmentSlot.Accessory2)
                || equippedItems.ContainsKey(EquipmentSlot.Accessory3);
        }

        return equippedItems.ContainsKey((EquipmentSlot)equipmentType);
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