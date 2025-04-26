#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class MegCooley : NPC
    {
        internal MegCooley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "meg_cooley")) { }
    }
}