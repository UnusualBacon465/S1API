#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class Ming : NPC
    {
        internal Ming() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "ming")) { }
    }
}