#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class ShirleyWatts : NPC
    {
        internal ShirleyWatts() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "shirley_watts")) { }
    }
}