#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    public class JackKnight : NPC
    {
        internal JackKnight() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "jack_knight")) { }
    }
}