#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    public class JeremyWilkinson : NPC
    {
        internal JeremyWilkinson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jeremy_wilkinson")) { }
    }
}