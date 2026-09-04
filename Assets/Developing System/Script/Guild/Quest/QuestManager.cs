using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    const int MaxQuestPerDay = 3;
    const int MaxBoardQuest = 8;
    const int MaxRandomRemoval = 5;

    QuestFactory questFactory;

    QuestData[] availableQuests;
    public IReadOnlyList<QuestData> AvailableQuests => availableQuests;

    Quest activeQuest;
    public Quest ActiveQuest => activeQuest;

    int completedQuestCount;
    public int CompletedQuestCount => completedQuestCount;
    public bool IsDailyCompleted => completedQuestCount >= MaxQuestPerDay;

    public delegate void OnQuestConfirmed();
    public event OnQuestConfirmed QuestConfirmed;

    public List<QuestData> questPool;

    private void Awake()
    {
        questFactory = new QuestFactory();
    }

    public void GenerateDailyBoard(List<QuestData> questPool)
    {
        availableQuests = questPool
            .Where(quest => quest != null)
            .OrderBy(_ => Random.value)
            .Take(MaxBoardQuest)
            .ToArray();
    }

    public bool ConfirmQuest(QuestData questData)
    {
        if (!CanStartQuest()) return false;

        if (availableQuests == null) return false;

        int selectedIndex = System.Array.IndexOf(
            availableQuests,
            questData
        );

        if (selectedIndex < 0)
            return false;

        activeQuest = questFactory.Create(questData);

        availableQuests[selectedIndex] = null;

        List<int> availableIndices = GetAvailableQuestIndices();

        int maxRemoval = Mathf.Min(
            MaxRandomRemoval,
            availableIndices.Count
        );

        if (maxRemoval > 0)
        {
            int removalCount = Random.Range(1, maxRemoval + 1);

            availableIndices = availableIndices
                .OrderBy(_ => Random.value)
                .Take(removalCount)
                .ToList();

            foreach (int index in availableIndices)
            {
                availableQuests[index] = null;
            }
        }

        QuestConfirmed?.Invoke();

        return true;
    }

    void RemoveQuest(QuestData quest)
    {
        for (int i = 0; i < availableQuests.Length; i++)
        {
            if (availableQuests[i] == quest)
            {
                availableQuests[i] = null;
                return;
            }
        }
    }

    List<int> GetAvailableQuestIndices()
    {
        return availableQuests
            .Select((quest, index) => new { quest, index })
            .Where(x => x.quest != null)
            .Select(x => x.index)
            .ToList();
    }

    bool CanStartQuest()
    {
        return activeQuest == null && completedQuestCount < MaxQuestPerDay;
    }

    public bool CanSelectQuest()
    {
        return completedQuestCount < MaxQuestPerDay;
    }

    public void CompleteQuest()
    {
        if (activeQuest == null) return;

        completedQuestCount++;
        activeQuest = null;
    }

    public void ResetDailyProgress()
    {
        activeQuest = null;
        completedQuestCount = 0;
    }
}