using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentStat", menuName = "Equipment/Equipment Stat")]
public class EquipmentStat : ScriptableObject
{
    [SerializedDictionary("Stat Type", "Value")]
    public SerializedDictionary<StatType, int> statBonuses = new SerializedDictionary<StatType, int>();
}