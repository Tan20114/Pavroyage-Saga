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

    [SerializeField] List<QuestData> questPool;

    private void Awake()
    {
        questFactory = new QuestFactory();
    }

    #region Quest Board
    public void GenerateDailyBoard()
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

        if (selectedIndex < 0) return false;

        activeQuest = questFactory.Create(questData);
        availableQuests[selectedIndex] = null;

        List<int> availableIndices = GetAvailableQuestIndices();

        int questsNeeded = MaxQuestPerDay - (completedQuestCount + 1);

        int maxRemoval = Mathf.Min(
            MaxRandomRemoval,
            availableIndices.Count - questsNeeded
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

        if (IsDailyCompleted)
        {
            StartNewDay();
            Debug.Log("Daily quest limit reached. Starting a new day.");
        }
    }

    public void StartNewDay()
    {
        activeQuest = null;
        completedQuestCount = 0;

        GenerateDailyBoard();
    }
    #endregion

    #region Stat Check Calculation
    public QuestOutcome ResolveStatCheck(Player player, int act)
    {
        if (activeQuest == null) return null;

        if (activeQuest.Data.questType != QuestType.StatCheck) return null;

        StatCheckCalculator calculator = new StatCheckCalculator();

        bool success = calculator.Check(player, activeQuest, act);

        return success ? activeQuest.Data.successOutcome : activeQuest.Data.failureOutcome;
    }

    void ExecuteOutcome(QuestOutcome outcome)
    {
        switch (outcome.type)
        {
            case QuestOutcomeType.Battle:
                // TODO: Start Battle
                break;

            case QuestOutcomeType.Reward:
                // TODO
                break;

            case QuestOutcomeType.NextQuest:
                // TODO
                break;

            case QuestOutcomeType.Story:
                // TODO
                break;
        }
    }
    #endregion
}