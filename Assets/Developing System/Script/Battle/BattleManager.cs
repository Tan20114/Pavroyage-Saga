using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BattleStateMachine))]
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("Reference")]
    [SerializeField] BattleUI battleUI;
    [SerializeField] List<Player> playerCharacters;
    public List<Player> PlayerCharacter => playerCharacters;
    [SerializeField] List<Monster> monsterCharacters;
    public List<Monster> MonsterCharacters => monsterCharacters;
    List<Character> AllCharacter
    {
        get { return playerCharacters.Cast<Character>().Concat(monsterCharacters.Cast<Character>()).ToList(); }
    }

    TurnManager turnManager;
    [SerializeField] BattleStateMachine stateMachine;
    [SerializeField] TargetSelectionInputHandler targetSelectionInputHandler;
    PlayerTurnController playerTurnController;
    BattleFlow battleFlow;

    Character turnOwner;
    public Character TurnOwner
    {
        get
        {
            return turnOwner;
        }

        private set
        {
            turnOwner = value;
        }
    }

    private void Awake()
    {
        Instance = this;

        battleFlow = GetComponent<BattleFlow>();
        turnManager = new TurnManager();
        targetSelectionInputHandler.Initialize(playerCharacters,monsterCharacters);
        playerTurnController = new PlayerTurnController(battleUI, targetSelectionInputHandler);
        battleUI.Initialize(playerTurnController);
        stateMachine = GetComponent<BattleStateMachine>();
    }

    void StartBattle()
    {
        battleFlow.StartBattle();

        foreach (Character character in AllCharacter)
        {
            character.onDead += OnCharacterDeathHandler;
        }

        stateMachine.ChangeState(new BattleStartState(turnManager, AllCharacter));
    }

    void Start()
    {
        StartBattle();
    }

    private void OnDestroy()
    {
        foreach (Character character in AllCharacter)
        {
            character.onDead -= OnCharacterDeathHandler;
        }
    }

    public void NextTurn()
    {
        if (CheckBattleEnd()) return;

        if (!turnManager.HasNextTurn())
        {
            stateMachine.ChangeState(new BattleNewRoundState(turnManager, AllCharacter));

            return;
        }

        Character nextTurnOwner = turnManager.GetNextCharacter();

        if (nextTurnOwner == null)
        {
            stateMachine.ChangeState(new BattleNewRoundState(turnManager, AllCharacter));

            return;
        }

        if (nextTurnOwner is Player player)
        {
            stateMachine.ChangeState(new PlayerTurnState(player, playerTurnController, targetSelectionInputHandler));
        }
        else if (nextTurnOwner is Monster monster)
        {
            stateMachine.ChangeState(new MonsterTurnState(monster, playerCharacters, monsterCharacters));
        }
    }

    bool CheckBattleEnd()
    {
        if (IsMonsterDefeated())
        {
            stateMachine.ChangeState(new VictoryState(battleFlow));
            return true;
        }

        if (IsPlayerDefeated())
        {
            stateMachine.ChangeState(new DefeatState(battleFlow));
            return true;
        }

        return false;
    }

    public void AddMonster(Monster monster)
    {
        monsterCharacters.Add(monster);
    }

    #region Status Check
    bool IsPlayerDefeated()
    {
        return playerCharacters.All(character => character.IsDead);
    }

    bool IsMonsterDefeated()
    {
        return monsterCharacters.All(character => character.IsDead);
    }
    #endregion

    #region Event Handler
    void OnCharacterDeathHandler(Character character)
    {
        turnManager.RemoveCharacter(character);
        CheckBattleEnd();
    }

    public void TestSummon(Character character)
    {
        turnManager.AddCharacter(character);

        stateMachine.ChangeState(new BattleNewRoundState(turnManager, AllCharacter));
    }

    public void SetTurnOwner(Character character)
    {
        TurnOwner = character;
    }
    #endregion
}
