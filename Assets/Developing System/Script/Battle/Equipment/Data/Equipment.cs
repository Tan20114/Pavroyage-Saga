using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Helmet,
    BodyArmor,
    Leggings,
    Boots,
    Accessory1,
    Accessory2,
    Accessory3
}

[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Equipment Data")]
public class Equipment : ScriptableObject
{
    public EquipmentType type;
    public EquipmentStat stats;
}
