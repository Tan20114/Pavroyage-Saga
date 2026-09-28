using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using UnityEngine;
using static EquippingHandler;

public class StatVisual : MonoBehaviour
{
    PlayerManager playerManager;
    CharacterStats baseStat;
    CharacterDerivedStat derivedStat;
    PlayerEquipment playerEquipment;
    EquippingHandler equippingHandler;

    [SerializedDictionary("Base Stats Types", "Text Renderer")]
    [SerializeField] SerializedDictionary<StatType, TextMeshProUGUI> baseStatTextRenderers;
    [SerializedDictionary("Derived Stats Types", "Text Renderer")]
    [SerializeField] SerializedDictionary<DerivedType, TextMeshProUGUI> derivedStatTextRenderers;

    private void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        equippingHandler = FindAnyObjectByType<EquippingHandler>();

        baseStat = GetComponent<CharacterStats>();
        derivedStat = GetComponent<CharacterDerivedStat>();
        playerEquipment = GetComponent<PlayerEquipment>();

        PlayerData playerData = playerManager.GetPlayerData(1);

        baseStat.LoadData(playerData.stats);
        derivedStat.LoadData(playerData.stats.derivedStat);
        playerEquipment.LoadData(playerData.equippedItems);
    }

    private void OnEnable()
    {
        equippingHandler.onEquipmentChanged += OnEquipmentChange;

        RecalculateStat();
    }

    private void OnDisable()
    {
        equippingHandler.onEquipmentChanged -= OnEquipmentChange;
    }

    private void LateUpdate()
    {
        foreach (StatType baseStatType in Enum.GetValues(typeof(StatType)))
        {
            baseStatTextRenderers[baseStatType].text = baseStat.GetStat(baseStatType).ToString();
        }

        foreach (DerivedType derivedStatType in Enum.GetValues(typeof(DerivedType)))
        {
            derivedStatTextRenderers[derivedStatType].text = derivedStat.GetDerived(derivedStatType).ToString();
        }
    }

    private void OnEquipmentChange(Equipment current, Equipment previous)
    {
        PlayerData playerData = playerManager.GetPlayerData(1);

        playerEquipment.LoadData(playerData.equippedItems);

        RecalculateStat();
    }

    void RecalculateStat()
    {
        playerEquipment.CalculateLastStat(baseStat,derivedStat);
        derivedStat.CalculatePlayerStat();
    }
}
