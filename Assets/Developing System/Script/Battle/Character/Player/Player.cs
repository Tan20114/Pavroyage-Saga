using UnityEngine;

[RequireComponent(typeof(CharacterDerivedStat))]
[RequireComponent(typeof(PlayerEquipment))]
public class Player : Character
{
    PlayerData playerData;

    int currentLevel = 1;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        private set
        {
            currentLevel = value;
        }
    }

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

    protected void OnEnable()
    {
        playerEquipment.onEquipmentChange += OnEquipmentChange;

        RecalculateStats();
        InitialHP();
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

    public void Initialize(PlayerData data)
    {
        playerData = data;

        name = data.characterName;

        baseStats.LoadData(data.stats);
        derivedStats.LoadData(data.stats.derivedStat);

        playerEquipment.LoadData(data.equippedItems);

        skillList = data.skills;

        RecalculateStats();
        InitialHP();
    }
}