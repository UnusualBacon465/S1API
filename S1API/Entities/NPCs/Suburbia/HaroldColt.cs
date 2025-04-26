#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    public class HaroldColt : NPC
    {
        internal HaroldColt() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "harold_colt")) { }
    }
}