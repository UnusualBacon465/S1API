#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class SamThompson : NPC
    {
        internal SamThompson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "sam_thompson")) { }
    }
}