using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] ItemData item;
    [SerializeField] int quantity;

    public ItemData Item => item;
    public int Quantity => quantity;

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;

        quantity += amount;
    }

    public bool Remove(int amount)
    {
        if (amount <= 0 || quantity < amount) return false;

        quantity -= amount; return true;
    }
}