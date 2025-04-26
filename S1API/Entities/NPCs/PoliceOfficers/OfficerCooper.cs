#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    public class OfficerCooper : NPC
    {
        internal OfficerCooper() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officer_cooper")) { }
    }
}