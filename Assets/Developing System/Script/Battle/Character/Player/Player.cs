using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Character
{
    [Header("Modifier")]
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] bool isBlocking = false;

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

        RecalculateStats();
        base.OnEnable();
    }

    private void OnDisable()
    {
        playerEquipment.onEquipmentChange -= OnEquipmentChange;
    }

    void OnEquipmentChange(Equipment current, Equipment previous, bool equipped)
    {
        RecalculateStats();
    }

    void RecalculateStats()
    {
        playerEquipment.CalculateLastStat(baseStats, derivedStats);
        derivedStats.CalculatePlayerStat();
    }

    public void SetBlocking(bool value)
    {
        IsBlocking = value;
    }
}