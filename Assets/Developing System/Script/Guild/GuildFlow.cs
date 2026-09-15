using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GuildFlow : MonoBehaviour
{
    QuestManager questManager => FindAnyObjectByType<QuestManager>();

    [SerializeField] SceneLoader sceneLoader;

    [SerializedDictionary("State", "Panel")]
    [SerializeField] SerializedDictionary<GuildState, GameObject> stateToPanel;

    public void ChangeState(GuildState state)
    {
        sceneLoader.StateTransition((int)state);
        GuildManager.Instance.guildState = state;

        foreach (var kvp in stateToPanel)
        {
            kvp.Value.SetActive(kvp.Key == state);
        }
    }

    public void GoToBattle()
    {
        if (questManager.ActiveQuest == null) return;

        sceneLoader.SceneTransition(1);
    }

    public void ExitGame()
    {
        sceneLoader.ExitGame();
    }

    public void ReloadScene()
    {
        sceneLoader.SceneTransition(0);
    }
}
