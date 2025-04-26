#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class DorisLubbin : NPC
    {
        internal DorisLubbin() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "doris_lubbin")) { }
    }
}