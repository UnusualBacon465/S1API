using System;
using HarmonyLib;
using UnityEngine.SceneManagement;
using S1API.Internal.Utils;
using S1API.Internal.Abstraction;
using S1API.PhoneApp;

namespace S1API.Internal.Patches
{   
#if IL2CPP
    [HarmonyPatch(typeof(SceneManager), nameof(SceneManager.Internal_SceneLoaded))]
#else
    [HarmonyPatch(typeof(SceneManager), "Internal_SceneLoaded", new Type[] { typeof(Scene), typeof(LoadSceneMode) })]

#endif
    internal static class PhoneAppPatches
    {
        // TODO (@omar-akermi): Can you look into if this is still needed pls?
        private static bool _loaded = false;

        /// <summary>
        /// Executes logic after the Unity SceneManager completes loading a scene.
        /// Registers all derived implementations of the PhoneApp class during the loading
        /// process of the "Main" scene.
        /// </summary>
        /// <param name="scene">The scene that has been loaded.</param>
        /// <param name="mode">The loading mode used by the SceneManager.</param>
        static void Postfix(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "Main") return;

            // Re-register all PhoneApps every time the Main scene loads
            var phoneApp = ReflectionUtils.GetDerivedClasses<PhoneApp.PhoneApp>();
            foreach (var type in phoneApp)
            {
                if (type.GetConstructor(Type.EmptyTypes) == null) continue;

                try
                {
                    var instance = (PhoneApp.PhoneApp)Activator.CreateInstance(type)!;
                    ((IRegisterable)instance).CreateInternal();
                }
                catch (System.Exception e)
                {
                    MelonLoader.MelonLogger.Warning($"[PhoneApp] Failed to register {type.FullName}: {e.Message}");
                }
            }
        }
    }
}