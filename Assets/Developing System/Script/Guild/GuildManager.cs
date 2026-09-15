using UnityEngine;

public enum GuildState
{
    MainMenu,
    CharacterSelection,
    GuildHub,
    Shop,
    Questboard,
    GuildMaster,
    Room
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

    public void GoToGuildMaster()
    {
        guildFlow.ChangeState(GuildState.GuildMaster);
    }

    public void GoToShop()
    {
        guildFlow.ChangeState(GuildState.Shop);
    }

    public void GoToQuestboard()
    {
        guildFlow.ChangeState(GuildState.Questboard);
    }

    public void GoToRoom()
    {
        guildFlow.ChangeState(GuildState.Room);
    }

    public void ExitGame()
    {
        guildFlow.ExitGame();
    }

    public void ReloadScene()
    {
        guildFlow.ReloadScene();
    }

    public void GoToBattle()
    {
        guildFlow.GoToBattle();
    }
}