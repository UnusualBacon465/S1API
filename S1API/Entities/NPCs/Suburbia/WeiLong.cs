#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Suburbia
{
    public class WeiLong : NPC
    {
        internal WeiLong() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "wei_long")) { }
    }
}