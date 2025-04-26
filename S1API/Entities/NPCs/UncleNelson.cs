#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs
{
    public class UncleNelson : NPC
    {
        internal UncleNelson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "uncle_nelson")) { }
    }
}