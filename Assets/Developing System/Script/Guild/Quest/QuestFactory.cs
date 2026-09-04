public class QuestFactory
{
    public Quest Create(QuestData data)
    {
        return new Quest(data);
    }
}