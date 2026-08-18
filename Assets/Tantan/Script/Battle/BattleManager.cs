using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BattleStateMachine))]
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [SerializeField] List<Character> playerCharacters;
    [SerializeField] List<Character> monsterCharacters;
    List<Character> AllCharacter
    {
        get { return playerCharacters.Concat(monsterCharacters).ToList(); }
    }

    TurnManager turnManager;
    [SerializeField] BattleStateMachine stateMachine;

    private void Awake()
    {
        Instance = this;

        turnManager = new TurnManager();
        stateMachine = GetComponent<BattleStateMachine>();
    }

    private void OnEnable()
    {
        foreach (Character character in AllCharacter)
        {
            character.onDead += OnCharacterDeathHandler;
        }
    }

    private void OnDisable()
    {
        foreach (Character character in AllCharacter)
        {
            character.onDead -= OnCharacterDeathHandler;
        }
    }

    void Start()
    {
        stateMachine.ChangeState(new BattleStartState(turnManager, AllCharacter));
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
            stateMachine.ChangeState(new PlayerTurnState(player, playerCharacters, monsterCharacters));
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
            stateMachine.ChangeState(new VictoryState());
            return true;
        }

        if (IsPlayerDefeated())
        {
            stateMachine.ChangeState(new DefeatState());
            return true;
        }

        return false;
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
    #endregion
}
