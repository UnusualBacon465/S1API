#if (IL2CPP)
using S1Loaders = Il2CppScheduleOne.Persistence.Loaders;
using S1NPCs = Il2CppScheduleOne.NPCs;
using Il2CppSystem.Collections.Generic;
#elif (MONO)
using S1Loaders = ScheduleOne.Persistence.Loaders;
using S1NPCs = ScheduleOne.NPCs;
using System.Collections.Generic;
#endif

using System;
using System.IO;
using System.Linq;
using HarmonyLib;
using S1API.Internal.Utils;
using S1API.NPCs;
using UnityEngine;

namespace S1API.Internal.Patches
{
    /// <summary>
    /// INTERNAL: All patches related to NPCs.
    /// </summary>
    [HarmonyPatch]
    internal class NPCPatches
    {
        
        // ReSharper disable once RedundantNameQualifier
        /// <summary>
        /// List of all custom NPCs currently created.
        /// </summary>
        private static System.Collections.Generic.List<NPC> _npcs = new System.Collections.Generic.List<NPC>();
        
        /// <summary>
        /// Patching performed for when game NPCs are loaded.
        /// </summary>
        /// <param name="__instance">NPCsLoader</param>
        /// <param name="mainPath">Path to the base NPC folder.</param>
        [HarmonyPatch(typeof(S1Loaders.NPCsLoader), "Load")]
        [HarmonyPrefix]
        private static void NPCsLoadersLoad(S1Loaders.NPCsLoader __instance, string mainPath)
        {
            foreach (Type type in ReflectionUtils.GetDerivedClasses<NPC>())
            {
                GameObject gameObject = new GameObject(type.Name);
                NPC customNPC = (NPC)Activator.CreateInstance(type);
                customNPC.InitializeInternal(gameObject);
                _npcs.Add(customNPC);
                string npcPath = Path.Combine(mainPath, customNPC.S1NPC.SaveFolderName);
                customNPC.LoadInternal(npcPath);
            }
        }
        
        /// <summary>
        /// Patching performed for when a single NPC starts (including modded in NPCs).
        /// </summary>
        /// <param name="__instance">Instance of the NPC</param>
        [HarmonyPatch(typeof(S1NPCs.NPC), "Start")]
        [HarmonyPostfix]
        private static void NPCStart(S1NPCs.NPC __instance) => 
            _npcs.FirstOrDefault(npc => npc.S1NPC == __instance)?.StartInternal();

        /// <summary>
        /// Patching performed for when an NPC calls to save data.
        /// </summary>
        /// <param name="__instance">Instance of the NPC</param>
        /// <param name="parentFolderPath">Path to this NPCs folder.</param>
        /// <param name="__result"></param>
        [HarmonyPatch(typeof(S1NPCs.NPC), "WriteData")]
        [HarmonyPostfix]
        private static void NPCWriteData(S1NPCs.NPC __instance, string parentFolderPath, ref List<string> __result)
        {
            System.Collections.Generic.List<string> list = __result.ToArray().ToList();
            _npcs.FirstOrDefault(npc => npc.S1NPC == __instance)?.SaveInternal(parentFolderPath, ref list);
        }
        
        /// <summary>
        /// Patching performed for when an NPC is destroyed.
        /// </summary>
        /// <param name="__instance">Instance of the NPC</param>
        [HarmonyPatch(typeof(S1NPCs.NPC), "OnDestroy")]
        [HarmonyPostfix]
        private static void NPCOnDestroy(S1NPCs.NPC __instance)
        {
            _npcs.RemoveAll(npc => npc.S1NPC == __instance);
            NPC? npc = _npcs.FirstOrDefault(npc => npc.S1NPC == __instance);
            if (npc == null)
                return;
            
            _npcs.Remove(npc);
        }
    }
}