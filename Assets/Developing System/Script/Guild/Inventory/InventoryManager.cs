using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    const int EquipmentCapacity = 50;
    const int StackableCapacity = 30;

    #region Equipment Inventory
    EquipmentInventory weaponInventory;
    EquipmentInventory helmetInventory;
    EquipmentInventory bodyArmorInventory;
    EquipmentInventory leggingsInventory;
    EquipmentInventory bootsInventory;
    EquipmentInventory accessoryInventory;
    #endregion

    #region Stackable Inventory
    StackableInventory consumableInventory;
    StackableInventory miscInventory;
    StackableInventory questItemInventory;
    #endregion

    private void Awake()
    {
        weaponInventory = new EquipmentInventory(EquipmentType.Weapon, EquipmentCapacity);
        helmetInventory = new EquipmentInventory(EquipmentType.Helmet, EquipmentCapacity);
        bodyArmorInventory = new EquipmentInventory(EquipmentType.BodyArmor, EquipmentCapacity);
        leggingsInventory = new EquipmentInventory(EquipmentType.Leggings, EquipmentCapacity);
        bootsInventory = new EquipmentInventory(EquipmentType.Boots, EquipmentCapacity);
        accessoryInventory = new EquipmentInventory(EquipmentType.Accessory, EquipmentCapacity);

        consumableInventory = new StackableInventory(ItemType.Consumable, StackableCapacity);
        miscInventory = new StackableInventory(ItemType.Misc, StackableCapacity);
        questItemInventory = new StackableInventory(ItemType.QuestItem, StackableCapacity);
    }

    public EquipmentInventory GetEquipmentInventory(EquipmentType type)
    {
        return type switch
        {
            EquipmentType.Weapon => weaponInventory,
            EquipmentType.Helmet => helmetInventory,
            EquipmentType.BodyArmor => bodyArmorInventory,
            EquipmentType.Leggings => leggingsInventory,
            EquipmentType.Boots => bootsInventory,
            EquipmentType.Accessory => accessoryInventory,
            _ => null
        };
    }

    public StackableInventory GetStackableInventory(ItemType type)
    {
        return type switch
        {
            ItemType.Consumable => consumableInventory,
            ItemType.Misc => miscInventory,
            ItemType.QuestItem => questItemInventory,
            _ => null
        };
    }

    public void AddItem(ItemData item, int quantity)
    {
        if (item == null || quantity <= 0) return;

        StackableInventory stackableInventory = GetStackableInventory(item.itemType);

        if (stackableInventory != null)
        {
            stackableInventory.AddItem(item, quantity);
        }
        else
        {
            Debug.LogWarning($"Item type {item.itemType} is not stackable.");
        }
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment == null) return;

        EquipmentInventory equipmentInventory = GetEquipmentInventory(equipment.type);
        if (equipmentInventory != null)
        {
            equipmentInventory.AddEquipment(equipment);
        }
        else
        {
            Debug.LogWarning($"Equipment type {equipment.type} is not valid.");
        }
    }

    public void RemoveItem(ItemData item, int quantity)
    {
        if (item == null || quantity <= 0) return;

        StackableInventory stackableInventory = GetStackableInventory(item.itemType);

        if (stackableInventory != null)
        {
            stackableInventory.RemoveItem(item, quantity);
        }
        else
        {
            Debug.LogWarning($"Item type {item.itemType} is not stackable.");
        }
    }

    public bool RemoveEquipment(Equipment equipment)
    {
        if (equipment == null) return false;

        EquipmentInventory equipmentInventory = GetEquipmentInventory(equipment.type);

        if (equipmentInventory == null)
        {
            Debug.LogWarning($"Equipment type {equipment.type} is not valid.");
            return false;
        }

        return equipmentInventory.RemoveEquipment(equipment);
    }
}