using UnityEngine;

public class EquipmentInventoryTest : MonoBehaviour
{
    [Header("Test Equipment")]
    [SerializeField] Equipment testEquipment;

    [Header("UI")]
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    private void Start()
    {
        AddEquipmentAndCreateButton();
    }

    [ContextMenu("Add Equipment & Create Button")]
    public void AddEquipmentAndCreateButton()
    {
        if (testEquipment == null)
        {
            Debug.LogError("Test Equipment is not assigned.");
            return;
        }

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found.");
            return;
        }

        // Add Equipment to Inventory
        EquipmentInventory inventory = inventoryManager.GetEquipmentInventory(testEquipment.type);

        if (inventory == null)
        {
            Debug.LogError(
                $"EquipmentInventory not found for type: {testEquipment.type}"
            );
            return;
        }

        bool added = inventory.AddEquipment(testEquipment);

        if (!added)
        {
            Debug.LogError(
                $"Failed to add {testEquipment.name} to {testEquipment.type} inventory."
            );
            return;
        }

        Debug.Log(
            $"Added {testEquipment.name} to {testEquipment.type} inventory."
        );

        // Create Button
        if (buttonPrefab == null || buttonParent == null)
        {
            Debug.LogWarning(
                "Equipment added, but Button Prefab or Button Parent is missing."
            );
            return;
        }

        EquipmentButtonData button = Instantiate(buttonPrefab, buttonParent).GetComponent<EquipmentButtonData>();

        button.SetData(testEquipment);

        Debug.Log($"Created button for {testEquipment.name}");
    }
}