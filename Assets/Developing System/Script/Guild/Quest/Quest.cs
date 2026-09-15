using UnityEngine;

public enum QuestOutcomeType
{
    Battle,
    Reward,
    NextQuest,
    Story
}

public enum BattleResult
{
    None,
    Victory,
    Defeat
}

public class Quest
{
    private readonly QuestData data;
    public QuestData Data {  get { return data; } }

    public Quest(QuestData data)
    {
        this.data = data;
    }
}

[System.Serializable]
public class QuestOutcome
{
    public QuestOutcomeType type;
}