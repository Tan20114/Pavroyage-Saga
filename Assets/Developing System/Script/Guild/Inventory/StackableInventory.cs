using System.Collections.Generic;

public class StackableInventory
{
    ItemType itemType;
    public ItemType Type { get { return itemType; } }

    Dictionary<ItemData, InventorySlot> inventorySlots = new Dictionary<ItemData, InventorySlot>();

    int capacity;
    public int Capacity => capacity;

    public StackableInventory(ItemType type, int cap)
    {
        itemType = type;
        capacity = cap;
    }

    public bool IsFull()
    {
        return inventorySlots.Count >= capacity;
    }

    public void AddItem(ItemData item, int quantity)
    {
        if (item.itemType != itemType) return;
        if (item == null || quantity <= 0) return;

        if (inventorySlots.TryGetValue(item, out InventorySlot slot))
        {
            slot.Add(quantity);
            return;
        }

        if (inventorySlots.Count >= capacity) return;

        inventorySlots.Add(item, new InventorySlot(item, quantity));
    }

    public void RemoveItem(ItemData item, int quantity)
    {
        if (item.itemType != itemType) return;
        if (item == null || quantity <= 0) return;

        InventorySlot slot = null;

        if (inventorySlots.TryGetValue(item, out slot))
        {
            if (slot.Remove(quantity))
            {
                if (slot.Quantity <= 0)
                {
                    inventorySlots.Remove(item);
                }
            }
        }
    }

    public bool ContainsItem(ItemData item)
    {
        return item != null && inventorySlots.ContainsKey(item);
    }

    public InventorySlot GetItem(ItemData item)
    {
        if (item == null) return null;

        InventorySlot slot = null;

        inventorySlots.TryGetValue(item, out slot);

        return slot;
    }

    public List<InventorySlot> GetAllItems()
    {
        return new List<InventorySlot>(inventorySlots.Values);
    }
}
