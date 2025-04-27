#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ScriptableObjects = Il2CppScheduleOne.ScriptableObjects;
using S1Quests = Il2CppScheduleOne.Quests;
using S1Variables = Il2CppScheduleOne.Variables;
#elif (MONOMELON || MONOBEPINEX)
using S1ScriptableObjects = ScheduleOne.ScriptableObjects;
using S1Quests = ScheduleOne.Quests;
using S1Variables = ScheduleOne.Variables;
#endif

using System;

using S1API.Conditions;
using S1API.Internal.Utils;
using S1API.PhoneCalls.Constants;

namespace S1API.PhoneCalls
{
    /// <summary>
    /// Represents a Stage entry in a call.
    /// </summary>
    public class CallStageEntry
    {
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// INTERNAL: The stored reference to the Stage entry in-game;
        /// </summary>
        internal readonly S1ScriptableObjects.PhoneCallData.Stage S1Stage;

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// @TODO: Docs
        /// </summary>
        protected SystemTriggerEntry[] StartTriggerEntries = Array.Empty<SystemTriggerEntry>();

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// @TODO: Docs
        /// </summary>
        protected SystemTriggerEntry[] DoneTriggerEntries = Array.Empty<SystemTriggerEntry>();

        /// <summary>
        /// INTERNAL: Creates a stage entry from an in-game stage instance.
        /// </summary>
        /// <param name="stage"></param>
        internal CallStageEntry(S1ScriptableObjects.PhoneCallData.Stage stage) =>
            S1Stage = stage;

        /// <summary>
        /// The text to display in this Stage
        /// </summary>
        public string Text
        {
            get => S1Stage.Text;
            set => S1Stage.Text = value;
        }

        /// <summary>
        /// Adds a start trigger to the <see cref="CallStageEntry"/> instance.
        /// </summary>
        /// <param name="triggerType">The <see cref="SystemTriggerType"/> this trigger has to be added to</param>
        /// <returns></returns>
        public SystemTriggerEntry AddSystemTrigger(SystemTriggerType triggerType)
        {
            S1Quests.SystemTrigger originalTrigger = new S1Quests.SystemTrigger();
            SystemTriggerEntry systemTriggerEntry = new SystemTriggerEntry(originalTrigger);

            switch (triggerType)
            {
                case SystemTriggerType.StartTrigger:
                    ArrayUtils.Add(ref S1Stage.OnStartTriggers, originalTrigger);
                    ArrayUtils.Add(ref StartTriggerEntries, systemTriggerEntry);
                    break;
                case SystemTriggerType.DoneTrigger:
                    ArrayUtils.Add(ref S1Stage.OnDoneTriggers, originalTrigger);
                    ArrayUtils.Add(ref DoneTriggerEntries, systemTriggerEntry);
                    break;
            }

            return systemTriggerEntry;
        }
    }
}
