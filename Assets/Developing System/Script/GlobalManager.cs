using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    public QuestManager QuestManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

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
}