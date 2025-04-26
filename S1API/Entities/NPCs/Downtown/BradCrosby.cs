#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Downtown
{
    public class BradCrosby : NPC
    {
        internal BradCrosby() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "brad_crosby")) { }
    }
}