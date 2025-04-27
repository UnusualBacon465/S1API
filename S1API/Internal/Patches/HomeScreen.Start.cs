using System;
using HarmonyLib;
using Il2CppScheduleOne.UI.Phone;
using UnityEngine.SceneManagement;
using S1API.Internal.Utils;
using S1API.Internal.Abstraction;
using S1API.PhoneApp;

namespace S1API.Internal.Patches;

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
        /// <summary>
        /// Postfix method to modify the behavior of the HomeScreen's Start method after it is executed.
        /// </summary>
        /// <remarks>
        /// This method iterates over the list of registered phone applications and performs initialization for each app.
        /// It ensures that the UI panel and the app icon for each application are created and properly configured.
        /// If an error occurs during this process, a warning is logged with relevant details.
        /// </remarks>
        static void Postfix()
        {
            foreach (var app in PhoneAppRegistry.RegisteredApps)
            {
                try
                {
                    app.SpawnUI();    // Clone ProductManagerApp, clear container, call OnCreatedUI
                    app.SpawnIcon();  // Clone last HomeScreen icon, set label + icon image
                }
                catch (Exception e)
                {
                    MelonLoader.MelonLogger.Warning($"[PhoneApp] Failed to spawn UI for {app.GetType().Name}: {e.Message}");
                }
            }
        }
    }

    
}