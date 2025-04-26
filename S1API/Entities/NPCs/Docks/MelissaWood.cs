#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Docks
{
    public class MelissaWood : NPC
    {
        internal MelissaWood() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "melissa_wood")) { }
    }
}