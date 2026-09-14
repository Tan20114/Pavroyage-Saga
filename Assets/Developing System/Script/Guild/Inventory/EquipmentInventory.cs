using System.Collections.Generic;

public class EquipmentInventory
{
    EquipmentType equipmentType;
    public EquipmentType Type {get { return equipmentType; }}

    int capacity;
    public int Capacity => capacity;

    List<Equipment> equipmentList = new List<Equipment>();

    public EquipmentInventory(EquipmentType type, int capacity)
    {
        equipmentType = type;
        this.capacity = capacity;
    }

    public bool IsFull()
    {
        return equipmentList.Count >= capacity;
    }

    public bool AddEquipment(Equipment equipment)
    {
        if (equipment == null || equipment.type != equipmentType) return false;

        if (IsFull()) return false;

        equipmentList.Add(equipment);
        return true;
    }

    public bool RemoveEquipment(Equipment equipment)
    {
        if (equipment == null || equipment.type != equipmentType) return false;

        return equipmentList.Remove(equipment);
    }

    public bool ContainsEquipment(Equipment equipment)
    {
        return equipmentList.Contains(equipment);
    }

    public Equipment GetEquipment(string name)
    {
        return equipmentList.Find(e => e.name == name);
    }

    public Equipment GetEquipment(Equipment equipment)
    {
        return equipmentList.Find(e => e == equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return new List<Equipment>(equipmentList);
    }
}
