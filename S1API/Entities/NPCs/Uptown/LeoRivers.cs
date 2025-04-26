#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;
using NPC = S1API.Entities.NPC;

namespace S1API.Entities.NPCs.Uptown
{
    public class LeoRivers : NPC
    {
        internal LeoRivers() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "leo_rivers")) { }
    }
}