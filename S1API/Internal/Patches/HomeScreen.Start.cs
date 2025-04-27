using System;
using HarmonyLib;
using UnityEngine.SceneManagement;
using S1API.Internal.Utils;
using S1API.Internal.Abstraction;
using S1API.PhoneApp;
#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.UI.Phone;
#else
using ScheduleOne.UI.Phone;
#endif

namespace S1API.Internal.Patches
{
    /// <summary>
    /// The <c>HomeScreen_Start</c> class contains functionality that patches the
    /// HomeScreen's Start method using the Harmony library within the Il2CppScheduleOne UI.Phone namespace.
    /// This class is part of the S1API.Internal.Patches namespace, enabling modification or extension
    /// of the behavior of the HomeScreen component's Start method.
    /// </summary>
    public class HomeScreen_Start
    {
        /// <summary>
        /// Represents a patch class for modifying the behavior of the Start method in the HomeScreen class.
        /// This class is implemented as part of the Harmony patching mechanism to apply modifications
        /// or inject additional logic during the execution of the Start method.
        /// </summary>
        [HarmonyPatch(typeof(HomeScreen), "Start")]
        internal static class HomeScreen_Start_Patch
        {
            static void Postfix(HomeScreen __instance)
            {
                if (__instance == null)
                    return;

                foreach (var app in PhoneAppRegistry.RegisteredApps)
                {
                    app.SpawnUI(__instance);
                    app.SpawnIcon(__instance);
                }
            }
        }

    }
}