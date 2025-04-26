#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class JoyceBall : NPC
    {
        internal JoyceBall() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "joyce_ball")) { }
    }
}