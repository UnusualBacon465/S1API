#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    public class OfficerGreen : NPC
    {
        internal OfficerGreen() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officer_green")) { }
    }
}