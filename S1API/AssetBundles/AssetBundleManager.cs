using System.Reflection;

using UnityEngine;

namespace S1API.AssetBundles
{
    /// <summary>
    /// The asset bundle manager
    /// </summary>
    public static class AssetBundleManager
    {
#if IL2CPPMELON || IL2CPPBEPINEX
        /// <summary>
        /// Loads an Il2Cpp AssetBundle from an embedded resource stream by name.
        /// </summary>
        /// <param name="fullResourceName">The full embedded resource name (including namespace path).</param>
        /// <returns>The loaded Il2CppAssetBundle, or throws on failure.</returns>
        public static  WrappedAssetBundle GetAssetBundleFromStream(string fullResourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                    throw new Exception($"Embedded resource '{fullResourceName}' not found in {assembly.FullName}."); // hoping these throws will be melon/bepinex-agnostic

                // Read the stream into a byte array
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                // Load the AssetBundle from memory
                Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromMemory(data);

                if (bundle == null)
                {
                    MelonLoader.Logger.Error($"Failed to load AssetBundle from memory: {fullResourceName}");
                    throw new Exception($"Failed to load AssetBundle from memory: {fullResourceName}");
                }

                return new(bundle);
            }
        }
#elif MONOMELON || MONOBEPINEX
        /// <summary>
        /// Load a <see cref="WrappedAssetBundle"/> instance by <see cref="string"/> resource name.
        /// </summary>
        /// <param name="fullResourceName">The full embedded resource name (including namespace path);</param>
        /// <returns>The loaded AssetBundle instance</returns>
        public static WrappedAssetBundle GetAssetBundleFromStream(string fullResourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(fullResourceName);
            return new WrappedAssetBundle(AssetBundle.LoadFromStream(stream));
        }
#endif

        /// <summary>
        ///
        /// No need to type the assembly just the path stuff.
        ///
        /// Example if carassetbundle is in subfolder /bundles/:
        ///
        ///         GameObject myCar = Instantiate(EasyLoad<GameObject>("bundles.carassetbundle", "MyCarGameObject"));
        ///
        /// </summary>
        public static T EasyLoad<T>(string bundleName, string objectName) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, Assembly.GetExecutingAssembly(), out _);
        }

        public static T EasyLoad<T>(string bundleName, string objectName, out WrappedAssetBundle bundle) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, Assembly.GetExecutingAssembly(), out bundle);
        }

        public static T EasyLoad<T>(string bundleName, string objectName, Assembly assemblyOverride) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, assemblyOverride, out _);
        }

        public static T EasyLoad<T>(string bundleName, string objectName, Assembly assemblyOverride, out WrappedAssetBundle bundle) where T : Object
        {
            // Get the asset bundle from the assembly
            bundle = GetAssetBundleFromStream($"{assemblyOverride.GetName().Name}.{bundleName}");

            // Load the asset from the bundle
            return bundle.LoadAsset<T>(objectName);
        }

    }

}
