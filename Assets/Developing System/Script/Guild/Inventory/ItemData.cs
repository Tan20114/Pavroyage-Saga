using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Misc,
    QuestItem
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    public bool stackable;
}