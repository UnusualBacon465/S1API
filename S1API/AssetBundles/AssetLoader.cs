using System;
using System.Reflection;

#if IL2CPPBEPINEX || IL2CPPMELON
using System.IO;
#endif

using UnityEngine;
using Object = UnityEngine.Object;

using S1API.Logging;

namespace S1API.AssetBundles
{
    /// <summary>
    /// The asset bundle manager
    /// </summary>
    public static class AssetLoader
    {
        private static readonly Log _logger = new Log("AssetLoader");

#if IL2CPPMELON || IL2CPPBEPINEX
        /// <summary>
        /// Loads an Il2Cpp AssetBundle from an embedded resource stream by name.
        /// </summary>
        /// <param name="fullResourceName">The full embedded resource name (including namespace path).</param>
        /// <returns>The loaded Il2CppAssetBundle, or throws on failure.</returns>
        public static WrappedAssetBundle GetAssetBundleFromStream(string fullResourceName)
        {
            // Attempt to find the embedded resource in the executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            using Stream? stream = assembly.GetManifestResourceStream(fullResourceName);
            if (stream == null)
                throw new Exception($"Embedded resource '{fullResourceName}' not found in {assembly.FullName}."); // hoping these throws will be melon/bepinex-agnostic

            // Read the stream into a byte array
            byte[] data = new byte[stream.Length];
            _ = stream.Read(data, 0, data.Length);

            // Load the AssetBundle from memory
            Il2CppAssetBundle bundle = Il2CppAssetBundleManager.LoadFromMemory(data);
            if (bundle == null)
                throw new Exception($"Failed to load AssetBundle from memory: {fullResourceName}");

            return new WrappedAssetBundle(bundle);
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
        /// Loads an asset of type <typeparamref name="T"/> from an embedded AssetBundle using the executing assembly.
        /// </summary>
        /// <typeparam name="T">The type of asset to load (must derive from UnityEngine.Object).</typeparam>
        /// <param name="bundleName">The name of the embedded AssetBundle resource.</param>
        /// <param name="objectName">The name of the asset to load within the AssetBundle.</param>
        /// <returns>The loaded asset of type <typeparamref name="T"/>.</returns>
        public static T EasyLoad<T>(string bundleName, string objectName) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, Assembly.GetExecutingAssembly(), out _);
        }

        /// <summary>
        /// Loads an asset of type <typeparamref name="T"/> from an embedded AssetBundle using the executing assembly and outputs the loaded bundle.
        /// </summary>
        /// <typeparam name="T">The type of asset to load (must derive from UnityEngine.Object).</typeparam>
        /// <param name="bundleName">The name of the embedded AssetBundle resource.</param>
        /// <param name="objectName">The name of the asset to load within the AssetBundle.</param>
        /// <param name="bundle">The output parameter containing the loaded <see cref="WrappedAssetBundle"/>.</param>
        /// <returns>The loaded asset of type <typeparamref name="T"/>.</returns>
        public static T EasyLoad<T>(string bundleName, string objectName, out WrappedAssetBundle bundle) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, Assembly.GetExecutingAssembly(), out bundle);
        }

        /// <summary>
        /// Loads an asset of type <typeparamref name="T"/> from an embedded AssetBundle using a specified assembly.
        /// </summary>
        /// <typeparam name="T">The type of asset to load (must derive from UnityEngine.Object).</typeparam>
        /// <param name="bundleName">The name of the embedded AssetBundle resource.</param>
        /// <param name="objectName">The name of the asset to load within the AssetBundle.</param>
        /// <param name="assemblyOverride">The assembly from which to load the embedded AssetBundle resource.</param>
        /// <returns>The loaded asset of type <typeparamref name="T"/>.</returns>
        public static T EasyLoad<T>(string bundleName, string objectName, Assembly assemblyOverride) where T : Object
        {
            return EasyLoad<T>(bundleName, objectName, assemblyOverride, out _);
        }

        /// <summary>
        /// Loads an asset of type <typeparamref name="T"/> from an embedded AssetBundle using a specified assembly and outputs the loaded bundle.
        /// </summary>
        /// <typeparam name="T">The type of asset to load (must derive from UnityEngine.Object).</typeparam>
        /// <param name="bundleName">The name of the embedded AssetBundle resource.</param>
        /// <param name="objectName">The name of the asset to load within the AssetBundle.</param>
        /// <param name="assemblyOverride">The assembly from which to load the embedded AssetBundle resource.</param>
        /// <param name="bundle">The output parameter containing the loaded <see cref="WrappedAssetBundle"/>.</param>
        /// <returns>The loaded asset of type <typeparamref name="T"/>.</returns>
        public static T EasyLoad<T>(string bundleName, string objectName, Assembly assemblyOverride, out WrappedAssetBundle bundle) where T : Object
        {
            // Get the asset bundle from the assembly
            bundle = GetAssetBundleFromStream($"{assemblyOverride.GetName().Name}.{bundleName}");

            // Load the asset from the bundle
            return bundle.LoadAsset<T>(objectName);
        }
    }
}
