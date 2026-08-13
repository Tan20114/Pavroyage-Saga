using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializedDictionary("Equipment Type", "Equipment")]
    public SerializedDictionary<EquipmentType, Equipment> statModifiers;

    public void AddStatModifier(Equipment equipment) => statModifiers.Add(equipment.type, equipment);

    public void RemoveStatModifier(EquipmentType equipmentType) => statModifiers.Remove(equipmentType);
}