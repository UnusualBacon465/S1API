#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class JessiWaters : NPC
    {
        internal JessiWaters() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jessi_waters")) { }
    }
}