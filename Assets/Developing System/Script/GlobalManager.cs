using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    public QuestManager QuestManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

    public BattleResult LastBattleResult { get; private set; }

    public bool IsStart { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        QuestManager = GetComponent<QuestManager>();
        PlayerManager = GetComponent<PlayerManager>();

        DontDestroyOnLoad(gameObject);

        QuestManager.StartNewDay();
    }

    public void SetBattleResult(BattleResult result)
    {
        LastBattleResult = result;
    }

    public void StartGame()
    {
        IsStart = true;
    }
}