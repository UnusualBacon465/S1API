#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Westville
{
    public class CharlesRowland : NPC
    {
        internal CharlesRowland() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "charles_rowland")) { }
    }
}