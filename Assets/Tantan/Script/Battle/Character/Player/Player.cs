using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Character
{
    [Header("Modifier")]
    [SerializeField] PlayerEquipment playerEquipment;
    bool isBlocking = false;

    public bool IsBlocking 
    {
        get
        {
            return isBlocking;
        }

        private set
        {
            isBlocking = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        playerEquipment = GetComponent<PlayerEquipment>();
    }

    protected override void OnEnable()
    {
        playerEquipment.onEquipmentChange += OnEquipmentChange;

        playerEquipment.CalculateLastStat(baseStats, derivedStats);
        derivedStats.CalculatePlayerStat();

        base.OnEnable();
    }

    private void OnDisable()
    {
        playerEquipment.onEquipmentChange -= OnEquipmentChange;
    }

    void OnEquipmentChange(Equipment equipment, bool isEquipped)
    {
        playerEquipment.CalculateLastStat(baseStats, derivedStats);
        derivedStats.CalculatePlayerStat();
    }

    public void PlayerAttack()
    {
        Attack(this, DamageCategory.Physical, Element.Fire);
    }
}