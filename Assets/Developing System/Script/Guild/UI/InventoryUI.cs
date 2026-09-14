using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Initial Equipment")]
    [SerializeField] Equipment[] initialEquipment;

    [Header("UI")]
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonParent;

    InventoryManager inventoryManager;
    EquippingHandler equippingHandler;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        equippingHandler = FindAnyObjectByType<EquippingHandler>();
    }

    private void OnEnable()
    {
        if (equippingHandler != null)
            equippingHandler.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable()
    {
        if (equippingHandler != null)
            equippingHandler.onEquipmentChanged -= OnEquipmentChanged;
    }

    private void Start()
    {
        InitializeInventory();
        RefreshEquipmentButtons();
    }

    void InitializeInventory()
    {
        foreach (Equipment equipment in initialEquipment)
        {
            if (equipment == null)
                continue;

            EquipmentInventory inventory =
                inventoryManager.GetEquipmentInventory(equipment.type);

            if (inventory == null)
            {
                Debug.LogWarning(
                    $"No inventory found for {equipment.name}"
                );

                continue;
            }

            bool added = inventory.AddEquipment(equipment);

            if (!added)
            {
                Debug.LogWarning(
                    $"Failed to add {equipment.name} to inventory."
                );
            }
        }
    }

    void OnEquipmentChanged(
        Equipment newEquipment,
        Equipment oldEquipment)
    {
        RefreshEquipmentButtons();
    }

    void RefreshEquipmentButtons()
    {
        ClearButtons();

        foreach (Equipment equipment in initialEquipment)
        {
            if (equipment == null)
                continue;

            EquipmentInventory inventory =
                inventoryManager.GetEquipmentInventory(equipment.type);

            if (inventory == null)
                continue;

            if (!inventory.ContainsEquipment(equipment))
                continue;

            EquipmentButtonData button =
                Instantiate(buttonPrefab, buttonParent)
                    .GetComponent<EquipmentButtonData>();

            button.SetData(equipment);
        }
    }

    void ClearButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
    }
}