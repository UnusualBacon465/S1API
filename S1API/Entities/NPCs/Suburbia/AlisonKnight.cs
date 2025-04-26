#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    public class AlisonKnight : NPC
    {
        internal AlisonKnight() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "alison_knight")) { }
    }
}