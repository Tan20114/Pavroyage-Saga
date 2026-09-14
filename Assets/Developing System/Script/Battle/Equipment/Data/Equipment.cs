using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Helmet,
    BodyArmor,
    Leggings,
    Boots,
    Accessory
}

public enum EquipmentSlot
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

[CreateAssetMenu(menuName = "Item/Equipments/Equipment")]
public class Equipment : ItemData
{
    public EquipmentType type;
    public EquipmentStat stats;
}
