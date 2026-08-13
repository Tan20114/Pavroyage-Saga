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

public class Equipment : MonoBehaviour
{
    public EquipmentType type;
    public EquipmentStat equipmentStat;
}
