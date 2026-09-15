using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCard : MonoBehaviour
{
    QuestBoard questBoard;
    QuestData questData;

    Button questCardButton;
    Image questCardSprite;
    [SerializeField] Image[] difficultyStar;
    [SerializeField] Sprite[] starSprites;

    private void Awake()
    {
        questCardButton = GetComponent<Button>();
        questCardSprite = GetComponent<Image>();
    }

    public void Initialize(QuestBoard questBoard)
    {
        this.questBoard = questBoard;
    }

    public void SetQuest(QuestData questData)
    {
        this.questData = questData;

        // Update UI
        questCardSprite.sprite = questData.questImage;
        for (int i = 0; i < difficultyStar.Length; i++)
        {
            difficultyStar[i].sprite = starSprites[(i < questData.difficulty) ? 1 : 0];
        }
    }

    public void SelectQuest()
    {
        if (GuildManager.Instance.guildState != GuildState.Questboard) return;

        questBoard.SelectQuest(questData);
    }

    public void SetInteractable(bool interactable)
    {
        questCardButton.interactable = interactable;
    }
}