using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    public bool IsLocked
    {
        get
        {
            return GlobalManager.Instance.QuestManager.IsDailyCompleted;
        }
    }

    QuestData selectedQuest;
    public QuestData SelectedQuest => selectedQuest;

    [SerializeField] QuestCard[] questCards;

    private void Start()
    {
        QuestManager questManager = GlobalManager.Instance.QuestManager;

        questManager.QuestConfirmed += RefreshBoard;

        RefreshBoard();
    }

    private void OnDestroy()
    {
        if (GlobalManager.Instance != null && GlobalManager.Instance.QuestManager != null)
        {
            GlobalManager.Instance.QuestManager.QuestConfirmed -= RefreshBoard;
        }
    }

    void RefreshBoard()
    {
        IReadOnlyList<QuestData> quests = GlobalManager.Instance.QuestManager.AvailableQuests;

        if (quests == null) return;

        for (int i = 0; i < questCards.Length; i++)
        {
            QuestData quest = i < quests.Count ? quests[i] : null;

            if (quest != null)
            {
                questCards[i].Initialize(this);
                questCards[i].SetQuest(quest);
                questCards[i].SetInteractable(!IsLocked);
            }
            else
            {
                questCards[i].SetInteractable(false);
            }
        }
    }

    public void SelectQuest(QuestData quest)
    {
        if (!CanSelectQuest(quest)) return;

        selectedQuest = quest;
    }

    public bool CanSelectQuest(QuestData quest)
    {
        QuestManager questManager = GlobalManager.Instance.QuestManager;

        return questManager.CanSelectQuest()
            && questManager.AvailableQuests.Contains(quest);
    }

    public void ConfirmQuest()
    {
        if (selectedQuest == null)
            return;

        bool confirmed =
            GlobalManager.Instance.QuestManager.ConfirmQuest(selectedQuest);

        if (confirmed)
        {
            selectedQuest = null;
        }
    }
}