using UnityEngine;

public class EquippingHandlerTest : MonoBehaviour
{
    [Header("Test Equipment")]
    [SerializeField] private Equipment weaponA;
    [SerializeField] private Equipment weaponB;

    [SerializeField] private Equipment helmet;

    [SerializeField] private Equipment accessoryA;
    [SerializeField] private Equipment accessoryB;
    [SerializeField] private Equipment accessoryC;
    [SerializeField] private Equipment accessoryD;

    InventoryManager inventoryManager;
    EquippingHandler equippingHandler;
    PlayerManager playerManager;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        equippingHandler = FindAnyObjectByType<EquippingHandler>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    [ContextMenu("Run EquippingHandler Test")]

    private void Start()
    {
        RunTest();
    }

    public void RunTest()
    {
        Debug.Log("========== EQUIPPING HANDLER TEST START ==========");

        ResetEquipment();

        TestEquipWithoutInventory();
        TestEquipWeapon();
        TestReplaceWeapon();
        TestUnequipWeapon();

        TestAccessories();

        Debug.Log("========== EQUIPPING HANDLER TEST END ==========");
    }

    void TestEquipWithoutInventory()
    {
        Debug.Log("--- Test Equip Without Inventory ---");

        bool result = equippingHandler.EquipItem(weaponA);

        Debug.Log($"Expected: False");
        Debug.Log($"Actual: {result}");
    }

    void TestEquipWeapon()
    {
        Debug.Log("--- Test Equip Weapon ---");

        inventoryManager.AddEquipment(weaponA);

        bool result = equippingHandler.EquipItem(weaponA);

        PlayerData playerData = playerManager.GetPlayerData(1);

        bool correctSlot = playerData.equippedItems[EquipmentSlot.Weapon] == weaponA;

        EquipmentInventory inventory =
            inventoryManager.GetEquipmentInventory(EquipmentType.Weapon);

        bool removedFromInventory =
            !inventory.ContainsEquipment(weaponA);

        Debug.Log($"Equip result: {result}");
        Debug.Log($"Correct slot: {correctSlot}");
        Debug.Log($"Removed from inventory: {removedFromInventory}");
    }

    void TestReplaceWeapon()
    {
        Debug.Log("--- Test Replace Weapon ---");

        inventoryManager.AddEquipment(weaponB);

        bool result = equippingHandler.EquipItem(weaponB);

        PlayerData playerData = playerManager.GetPlayerData(1);

        Equipment equipped =
            playerData.equippedItems[EquipmentSlot.Weapon];

        EquipmentInventory inventory =
            inventoryManager.GetEquipmentInventory(EquipmentType.Weapon);

        bool newWeaponEquipped =
            equipped == weaponB;

        bool oldWeaponReturned =
            inventory.ContainsEquipment(weaponA);

        bool newWeaponRemoved =
            !inventory.ContainsEquipment(weaponB);

        Debug.Log($"Equip result: {result}");
        Debug.Log($"New weapon equipped: {newWeaponEquipped}");
        Debug.Log($"Old weapon returned to inventory: {oldWeaponReturned}");
        Debug.Log($"New weapon removed from inventory: {newWeaponRemoved}");
    }

    void TestUnequipWeapon()
    {
        Debug.Log("--- Test Unequip Weapon ---");

        bool result = equippingHandler.UnequipItem(weaponB);

        PlayerData playerData = playerManager.GetPlayerData(1);

        EquipmentInventory inventory =
            inventoryManager.GetEquipmentInventory(EquipmentType.Weapon);

        bool slotEmpty =
            playerData.equippedItems[EquipmentSlot.Weapon] == null;

        bool returnedToInventory =
            inventory.ContainsEquipment(weaponB);

        Debug.Log($"Unequip result: {result}");
        Debug.Log($"Weapon slot empty: {slotEmpty}");
        Debug.Log($"Weapon returned to inventory: {returnedToInventory}");
    }

    void TestAccessories()
    {
        Debug.Log("--- Test Accessories ---");

        inventoryManager.AddEquipment(accessoryA);
        inventoryManager.AddEquipment(accessoryB);
        inventoryManager.AddEquipment(accessoryC);
        inventoryManager.AddEquipment(accessoryD);

        bool resultA = equippingHandler.EquipItem(accessoryA);
        bool resultB = equippingHandler.EquipItem(accessoryB);
        bool resultC = equippingHandler.EquipItem(accessoryC);
        bool resultD = equippingHandler.EquipItem(accessoryD);

        PlayerData playerData = playerManager.GetPlayerData(1);

        bool slot1 =
            playerData.equippedItems[EquipmentSlot.Accessory1] == accessoryA;

        bool slot2 =
            playerData.equippedItems[EquipmentSlot.Accessory2] == accessoryB;

        bool slot3 =
            playerData.equippedItems[EquipmentSlot.Accessory3] == accessoryC;

        EquipmentInventory inventory =
            inventoryManager.GetEquipmentInventory(EquipmentType.Accessory);

        bool accessoryDStillInInventory =
            inventory.ContainsEquipment(accessoryD);

        Debug.Log($"Accessory A equip result: {resultA}");
        Debug.Log($"Accessory B equip result: {resultB}");
        Debug.Log($"Accessory C equip result: {resultC}");
        Debug.Log($"Accessory D equip result: {resultD}");

        Debug.Log($"Accessory1 correct: {slot1}");
        Debug.Log($"Accessory2 correct: {slot2}");
        Debug.Log($"Accessory3 correct: {slot3}");

        Debug.Log($"Accessory D still in inventory: {accessoryDStillInInventory}");
    }

    [ContextMenu("Unequip All")]
    public void UnequipAll()
    {
        PlayerData playerData = playerManager.GetPlayerData(1);

        Equipment[] equipped =
        {
            playerData.equippedItems[EquipmentSlot.Weapon],
            playerData.equippedItems[EquipmentSlot.Helmet],
            playerData.equippedItems[EquipmentSlot.Accessory1],
            playerData.equippedItems[EquipmentSlot.Accessory2],
            playerData.equippedItems[EquipmentSlot.Accessory3]
        };

        foreach (Equipment equipment in equipped)
        {
            if (equipment != null)
                equippingHandler.UnequipItem(equipment);
        }

        Debug.Log("Unequipped all test equipment.");
    }

    void ResetEquipment()
    {
        PlayerData playerData = playerManager.GetPlayerData(1);

        playerData.equippedItems[EquipmentSlot.Weapon] = null;
        playerData.equippedItems[EquipmentSlot.Helmet] = null;
        playerData.equippedItems[EquipmentSlot.BodyArmor] = null;
        playerData.equippedItems[EquipmentSlot.Leggings] = null;
        playerData.equippedItems[EquipmentSlot.Boots] = null;
        playerData.equippedItems[EquipmentSlot.Accessory1] = null;
        playerData.equippedItems[EquipmentSlot.Accessory2] = null;
        playerData.equippedItems[EquipmentSlot.Accessory3] = null;
    }
}