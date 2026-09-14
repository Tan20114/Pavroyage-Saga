using System.Collections.Generic;
using UnityEngine;

public class BattleFlow : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    Queue<GameObject> enemyQueue = new Queue<GameObject>();

    [SerializeField] Transform[] enemySlots = new Transform[5];
    [SerializeField] List<GameObject> enemyInSlot = new List<GameObject>();

    public void StartBattle()
    {
        Quest activeQuest = GlobalManager.Instance.QuestManager.ActiveQuest;

        if (activeQuest == null) return;

        foreach (GameObject enemy in activeQuest.Data.monsterPrefabs)
        {
            enemyQueue.Enqueue(enemy);
        }

        InitializePlayer();
        SpawnMonster();
    }

    void InitializePlayer()
    {
        for (int i = 0; i < BattleManager.Instance.PlayerCharacter.Count; i++)
        {
            Player player = BattleManager.Instance.PlayerCharacter[i];

            PlayerData data = GlobalManager.Instance.PlayerManager.GetPlayerData(i + 1);

            player.Initialize(data);
        }
    }

    void SpawnMonster()
    {
        while (enemyQueue.Count > 0)
        {
            GameObject enemy = enemyQueue.Dequeue();

            if (enemy == null) break;

            foreach (Transform slot in enemySlots)
            {
                if (slot.childCount == 0)
                {
                    GameObject enemyInstance = Instantiate(enemy, slot.position, Quaternion.identity, slot);
                    Monster monster = enemyInstance.GetComponent<Monster>();

                    monster.Initialize();

                    enemyInSlot.Add(enemyInstance);
                    break;
                }
            }
        }

        foreach (GameObject monster in enemyInSlot)
        {
            Debug.Log($"Adding monster: {monster.name} to BattleManager");
            BattleManager.Instance.AddMonster(monster.GetComponent<Monster>());
        }
    }

    public void GoToMainMenu()
    {
        sceneLoader.SceneTransition(0);
    }
}