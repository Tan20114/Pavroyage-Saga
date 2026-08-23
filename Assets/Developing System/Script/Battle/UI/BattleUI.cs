using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    PlayerTurnController turnController;

    [Header("Player UI")]
    [SerializeField] GameObject playerUIPanel;
    [SerializeField] GameObject baseActionPanel;
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject targetSelectionPanel;
    [SerializeField] Button attackButton;
    [SerializeField] Button skillButton;
    [SerializeField] Button skipButton;
    [SerializeField] Button[] skillActivationButtons;

    public void Initialize(PlayerTurnController ptc)
    {
        turnController = ptc;
    }

    void LateUpdate()
    {
        skipButton.interactable = turnController.CanExecute<SkipAction>();
        for (int index = 0 ; index < skillActivationButtons.Length ; index++)
        {
            skillActivationButtons[index].interactable = turnController.CanUseSkill(index);
        }
    }

    public void OpenPlayerPanel()
    {
        playerUIPanel.SetActive(true);
        baseActionPanel.SetActive(true);
        skillPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
    }

    public void ClosePlayerPanel()
    {
        playerUIPanel.SetActive(false);
        baseActionPanel.SetActive(false);
        skillPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
    }

    public void OnSkipButton()
    {
        turnController.SkipTurn();
    }

    public void OnAttackButton()
    {
        turnController.SelectAttack();
        baseActionPanel.SetActive(false);
        targetSelectionPanel.SetActive(true);
    }

    public void OnBlockButton()
    {
        turnController.Block();
        baseActionPanel.SetActive(false);
    }

    public void OnSkillButton()
    {
        baseActionPanel.SetActive(false);
        skillPanel.SetActive(true);
    }

    public void OnSkillActive(int index)
    {
        skillPanel.SetActive(false);
        turnController.SelectSkill(index);
        targetSelectionPanel.SetActive(true);
    }
}