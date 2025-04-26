#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class JerryMontero : NPC
    {
        internal JerryMontero() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jerry_montero")) { }
    }
}