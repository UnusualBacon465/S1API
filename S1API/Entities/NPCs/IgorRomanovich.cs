#if IL2CPP
using Il2CppScheduleOne.NPCs;
#else
using ScheduleOne.NPCs;
#endif
using System.Linq;

namespace S1API.Entities.NPCs
{
    public class IgorRomanovich : NPC
    {
        internal IgorRomanovich() : base(NPCManager.NPCRegistry.ToArray().First(n => n.ID == "igor_romanovich")) { }
    }
}