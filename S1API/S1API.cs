#if (MONOMELON || IL2CPPMELON)
using MelonLoader;

[assembly: MelonInfo(typeof(S1API.S1API), "S1API", "{VERSION_NUMBER}", "KaBooMa")]

namespace S1API
{
    /// <summary>
    /// Not currently utilized by S1API.
    /// </summary>
    public class S1API : MelonMod
    {
    }
}
#elif (IL2CPPBEPINEX || MONOBEPINEX)
using BepInEx;
#if MONOBEPINEX
using BepInEx.Unity.Mono;
#endif

using HarmonyLib;

namespace S1API
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class S1API : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("com.S1API").PatchAll();
        }
    }
}
#endif
