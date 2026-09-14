using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string characterName;
    public int level;

    public CharacterStatData stats;

    [SerializedDictionary("Equipment Slot", "Equipment")]
    public SerializedDictionary<EquipmentSlot, Equipment> equippedItems = new();

    public List<SkillData> skills;
}