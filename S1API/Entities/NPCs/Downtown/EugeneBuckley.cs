#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    public class EugeneBuckley : NPC
    {
        internal EugeneBuckley() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "eugene_buckley")) { }
    }
}