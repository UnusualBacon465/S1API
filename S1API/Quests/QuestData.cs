using S1API.Saveables;

namespace S1API.Quests
{
    /// <summary>
    /// INTERNAL: Default data saved for quests to keep the derived class known.
    /// </summary>
    class QuestData : SaveData
    {
        public readonly string QuestType;

        public QuestData() => QuestType = GetType().Name;
    }
}