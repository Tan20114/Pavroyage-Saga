using UnityEngine;

public class EquippingHandler : MonoBehaviour
{
    InventoryManager inventoryManager;
    PlayerManager playerManager;

    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public event OnEquipmentChanged onEquipmentChanged;

    void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    public bool EquipItem(Equipment equipment)
    {
        if (equipment == null) return false;

        EquipmentInventory inventory =
            inventoryManager.GetEquipmentInventory(equipment.type);

        if (!inventory.ContainsEquipment(equipment))
            return false;

        PlayerData playerData = playerManager.GetPlayerData(1);

        EquipmentSlot slot;
        Equipment oldEquipment = null;

        if (equipment.type == EquipmentType.Accessory)
        {
            EquipmentSlot? accessorySlot =
                GetAvailableAccessorySlot(playerData);

            if (!accessorySlot.HasValue)
                return false;

            slot = accessorySlot.Value;

            if (!inventory.RemoveEquipment(equipment))
                return false;
        }
        else
        {
            slot = GetEquipmentSlot(equipment.type);

            playerData.equippedItems.TryGetValue(
                slot,
                out oldEquipment
            );

            // Remove new equipment first
            if (!inventory.RemoveEquipment(equipment))
                return false;

            // Return old equipment to inventory
            if (oldEquipment != null)
            {
                EquipmentInventory oldInventory =
                    inventoryManager.GetEquipmentInventory(oldEquipment.type);

                if (!oldInventory.AddEquipment(oldEquipment))
                {
                    // Rollback
                    inventory.AddEquipment(equipment);
                    return false;
                }
            }
        }

        playerData.equippedItems[slot] = equipment;

        onEquipmentChanged?.Invoke(
            equipment,
            oldEquipment
        );

        Debug.Log(
            $"Equipped {equipment.name}" +
            (oldEquipment != null
                ? $" (Replaced {oldEquipment.name})"
                : "")
        );

        return true;
    }

    public bool UnequipItem(Equipment equipment)
    {
        if (equipment == null) return false;

        PlayerData playerData = playerManager.GetPlayerData(1);

        EquipmentSlot? slot = FindEquipmentSlot(playerData, equipment);

        if (!slot.HasValue) return false;

        EquipmentInventory inventory = inventoryManager.GetEquipmentInventory(equipment.type);

        if (!inventory.AddEquipment(equipment)) return false;

        playerData.equippedItems[slot.Value] = null;

        onEquipmentChanged?.Invoke(null, equipment);

        Debug.Log($"Unequipped {equipment.name}");
        return true;
    }

    EquipmentSlot GetEquipmentSlot(EquipmentType type)
    {
        return type switch
        {
            EquipmentType.Weapon => EquipmentSlot.Weapon,
            EquipmentType.Helmet => EquipmentSlot.Helmet,
            EquipmentType.BodyArmor => EquipmentSlot.BodyArmor,
            EquipmentType.Leggings => EquipmentSlot.Leggings,
            EquipmentType.Boots => EquipmentSlot.Boots,

            _ => throw new System.ArgumentException($"Equipment type {type} does not have a fixed equipment slot.")
        };
    }

    public Equipment GetEquippedEquipment(EquipmentSlot slot)
    {
        PlayerData playerData = playerManager.GetPlayerData(1);

        if (playerData.equippedItems.TryGetValue(slot, out Equipment equipment))
            return equipment;

        return null;
    }

    EquipmentSlot? GetAvailableAccessorySlot(PlayerData playerData)
    {
        if (playerData.equippedItems[EquipmentSlot.Accessory1] == null)
            return EquipmentSlot.Accessory1;

        if (playerData.equippedItems[EquipmentSlot.Accessory2] == null)
            return EquipmentSlot.Accessory2;

        if (playerData.equippedItems[EquipmentSlot.Accessory3] == null)
            return EquipmentSlot.Accessory3;

        return null;
    }

    EquipmentSlot? FindEquipmentSlot(PlayerData playerData, Equipment equipment)
    {
        foreach (var pair in playerData.equippedItems)
        {
            if (pair.Value == equipment)
                return pair.Key;
        }

        return null;
    }
}