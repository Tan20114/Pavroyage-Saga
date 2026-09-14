using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCard : MonoBehaviour
{
    QuestBoard questBoard;
    QuestData questData;

    Button questCardButton;
    TextMeshProUGUI questName;

    private void Awake()
    {
        questCardButton = GetComponent<Button>();
        questName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(QuestBoard questBoard)
    {
        this.questBoard = questBoard;
    }

    public void SetQuest(QuestData questData)
    {
        this.questData = questData;

        // Update UI
        questName.text = questData.questName;
    }

    public void SelectQuest()
    {
        questBoard.SelectQuest(questData);
    }

    public void SetInteractable(bool interactable)
    {
        questCardButton.interactable = interactable;
    }
}