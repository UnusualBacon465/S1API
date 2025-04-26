#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    public class PearlMoore : NPC
    {
        internal PearlMoore() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "pearl_moore")) { }
    }
}