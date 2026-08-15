using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Character
{
    [SerializeField] PlayerEquipment playerEquipment;

    protected override void Awake()
    {
        base.Awake();
        playerEquipment = GetComponent<PlayerEquipment>();
    }

    private void OnEnable()
    {
        playerEquipment.onEquipmentChange += OnEquipmentChange;
    }

    private void OnDisable()
    {
        playerEquipment.onEquipmentChange -= OnEquipmentChange;
    }

    void Start()
    {
        playerEquipment.CalculateLastStat(baseStats);
        derivedStats.CalculatePlayerStat();
    }

    void OnEquipmentChange(Equipment equipment, bool isEquipped)
    {
        playerEquipment.CalculateLastStat(baseStats);
        derivedStats.CalculatePlayerStat();
    }
}