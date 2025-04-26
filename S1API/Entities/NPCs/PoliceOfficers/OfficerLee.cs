#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    public class OfficerLee : NPC
    {
        internal OfficerLee() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officer_lee")) { }
    }
}