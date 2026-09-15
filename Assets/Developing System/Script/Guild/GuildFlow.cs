using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GuildFlow : MonoBehaviour
{
    QuestManager questManager => FindAnyObjectByType<QuestManager>();

    [SerializeField] SceneLoader sceneLoader;

    [SerializedDictionary("State", "Panel")]
    [SerializeField] SerializedDictionary<GuildState, GameObject> stateToPanel;

    private void Start()
    {
        if (GlobalManager.Instance.LastBattleResult == BattleResult.None && !GlobalManager.Instance.IsStart)
            ChangeState(GuildState.MainMenu);
        else if (GlobalManager.Instance.LastBattleResult == BattleResult.None)
            ChangeState(GuildState.GuildHub);
        else
        {
            switch (GlobalManager.Instance.LastBattleResult)
            {
                case BattleResult.Victory:
                    {
                        ChangeState(GuildState.VictoryResult);
                        break;
                    }
                case BattleResult.Defeat:
                    {
                        ChangeState(GuildState.DefeatResult);
                        break;
                    }
            }
        }
    }

    public void ChangeState(GuildState state)
    {
        if (GuildManager.Instance.guildState == state) return;

        GuildManager.Instance.guildState = state;

        foreach (var kvp in stateToPanel)
        {
            kvp.Value.SetActive(kvp.Key == state);
        }

        sceneLoader.StateTransition((int)state);
    }

    public void GoToBattle()
    {
        if (questManager.ActiveQuest == null) return;

        sceneLoader.SceneTransition(1);
    }

    public void StartGame()
    {
        GlobalManager.Instance.StartGame();
    }

    public void ExitGame()
    {
        sceneLoader.ExitGame();
    }

    public void ReloadScene()
    {
        sceneLoader.SceneTransition(0);
    }

    public void CompleteQuest()
    {
        GlobalManager.Instance.QuestManager.CompleteQuest();
    }
}
