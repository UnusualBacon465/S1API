#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class DonnaMartin : NPC
    {
        internal DonnaMartin() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "donna_martin")) { }
    }
}