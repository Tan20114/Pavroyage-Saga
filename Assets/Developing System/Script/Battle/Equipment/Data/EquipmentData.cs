using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipments/Equipment Stat")]
public class EquipmentStat : ScriptableObject
{
    [SerializedDictionary("Stat Type", "Value")]
    public SerializedDictionary<StatType, int> statBonuses = new SerializedDictionary<StatType, int>();

    [SerializedDictionary("Derived Type", "Value")]
    public SerializedDictionary<DerivedType, float> derivedBonuses = new SerializedDictionary<DerivedType, float>();
}