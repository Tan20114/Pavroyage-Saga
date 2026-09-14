using TMPro;
using UnityEngine;

public class EquipmentButtonData : MonoBehaviour
{
    EquippingHandler equippingHandler;

    [SerializeField] Equipment data;
    TextMeshProUGUI weaponNameText => GetComponentInChildren<TextMeshProUGUI>();

    private void Awake()
    {
        equippingHandler = FindAnyObjectByType<EquippingHandler>();
    }

    public void SetData(Equipment equipment)
    {
        data = equipment;
        weaponNameText.text = data.name;
    }

    public void OnClick()
    {
        if (data == null)
        {
            Debug.LogWarning("EquipmentButtonData: data is null.");
            return;
        }

        if (equippingHandler == null)
        {
            Debug.LogError("EquipmentButtonData: EquippingHandler not found.");
            return;
        }

        bool success = equippingHandler.EquipItem(data);

        if (success)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"Failed to equip {data.name}.");
        }
    }
}