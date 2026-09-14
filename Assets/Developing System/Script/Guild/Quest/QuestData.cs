using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Monster,
    StatCheck
}

public enum QuestRank
{
    D,
    C,
    B,
    A,
    S
}

[CreateAssetMenu(menuName = "Quest/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("Basic Information")]
    public string questName;

    [TextArea]
    [SerializeField] private string description;

    public Sprite questImage;

    [Header("Quest Info")]
    public QuestRank rank;

    [Range(1, 5)] public int difficulty;

    public QuestType questType;

    [Header("Stat Check")]
    public StatType statCheck;

    [Header("Outcome")]
    public QuestOutcome successOutcome;

    public QuestOutcome failureOutcome;

    [Header("Monster")]
    public List<GameObject> monsterPrefabs;
}
