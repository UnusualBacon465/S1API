#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class KathyHenderson : NPC
    {
        internal KathyHenderson() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "kathy_henderson")) { }
    }
}