using System;
using HarmonyLib;
using UnityEngine.SceneManagement;
using S1API.Internal.Utils;
using S1API.Internal.Abstraction;
using S1API.PhoneApp;
using S1API.Logging;

#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppScheduleOne.UI.Phone;
#else
using ScheduleOne.UI.Phone;
#endif

namespace S1API.Internal.Patches
{
    /// <summary>
    /// Patches related to PhoneApp system initialization and UI injection.
    /// </summary>
    public class HomeScreen_Start
    {
        private static readonly Log Logger = new Log("PhoneApp");

        /// <summary>
        /// Patches SceneManager scene loading to register all PhoneApps after Main scene loads.
        /// </summary>
#if (IL2CPPMELON || IL2CPPBEPINEX)
        [HarmonyPatch(typeof(SceneManager), nameof(SceneManager.Internal_SceneLoaded))]
#else
        [HarmonyPatch(typeof(SceneManager), "Internal_SceneLoaded", new Type[] { typeof(Scene), typeof(LoadSceneMode) })]
#endif
        internal static class PhoneAppPatches
        {
            static void Postfix(Scene scene, LoadSceneMode mode)
            {
                if (scene.name != "Main")
                    return;

                var phoneApps = ReflectionUtils.GetDerivedClasses<PhoneApp.PhoneApp>();
                foreach (var type in phoneApps)
                {
                    if (type.GetConstructor(Type.EmptyTypes) == null) continue;

                    try
                    {
                        var instance = (PhoneApp.PhoneApp)Activator.CreateInstance(type)!;
                        ((IRegisterable)instance).CreateInternal();
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Failed to create instance of {type.Name}: {e}");
                    }
                }
            }
        }

        /// <summary>
        /// Patches HomeScreen.Start to spawn registered PhoneApp UIs and icons.
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
