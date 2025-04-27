#if (IL2CPPMELON || IL2CPPBEPINEX)
using static Il2CppScheduleOne.Quests.QuestManager;
#elif (MONOMELON || MONOBEPINEX)
using static ScheduleOne.Quests.QuestManager;
#endif

namespace S1API.Quests.Constants
{
    /// <summary>
    /// A wrapper around <see cref="EQuestAction"/>
    /// </summary>
    public enum QuestAction
    {
        /// <summary>
        /// Begin the quest
        /// </summary>
        Begin,

        /// <summary>
        /// Succeed (complete) the quest
        /// </summary>
        Success,

        /// <summary>
        /// Fail the quest
        /// </summary>
        Fail,

        /// <summary>
        /// Expire the quest
        /// </summary>
        Expire,

        /// <summary>
        /// Cancel the quest
        /// </summary>
        Cancel
    }
}
