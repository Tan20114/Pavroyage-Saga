using UnityEngine;

public enum GuildState
{
    MainMenu,
    CharacterSelection,
    GuildHub
}

public class GuildManager : MonoBehaviour
{
    public static GuildManager Instance { get; private set; }

    [SerializeField] GuildFlow guildFlow;
    public GuildState guildState = GuildState.MainMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void GoToCharacterSelection()
    {
        guildFlow.ChangeState(GuildState.CharacterSelection);
    }

    public void GoToGuildHub()
    {
        guildFlow.ChangeState(GuildState.GuildHub);
    }

    public void GoToMainMenu()
    {
        guildFlow.ChangeState(GuildState.MainMenu);
    }

    public void GoToBattle()
    {
        guildFlow.GoToBattle();
    }
}