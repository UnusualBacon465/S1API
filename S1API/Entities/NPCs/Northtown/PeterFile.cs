#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs.Northtown
{
    public class PeterFile : NPC
    {
        internal PeterFile() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "peter_file")) { }
    }
}