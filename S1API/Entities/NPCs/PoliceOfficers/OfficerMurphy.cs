#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.PoliceOfficers
{
    public class OfficerMurphy : NPC
    {
        internal OfficerMurphy() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "officer_murphy")) { }
    }
}