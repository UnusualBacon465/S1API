#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif 

using System.Linq;

namespace S1API.NPCs
{
    public class AlbertHoover : NPC
    {
        internal AlbertHoover() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "albert_hoover")) { }
    }
}