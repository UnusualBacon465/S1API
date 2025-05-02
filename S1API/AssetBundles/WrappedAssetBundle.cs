using UnityEngine;

#if IL2CPPMELON
using Il2CppSystem;
using AssetBundle = UnityEngine.Il2CppAssetBundle;
using AssetBundleRequest = UnityEngine.Il2CppAssetBundleRequest;
#else
using System;
#endif

using Object = UnityEngine.Object;

namespace S1API.AssetBundles
{
    /// <summary>
    /// INTERNAL: Wrapper around <see cref="AssetBundle"/> instance.
    /// </summary>
    public class WrappedAssetBundle
    {
        public bool IsStreamedAssetBundle => _realBundle.isStreamedSceneAssetBundle;

        public AssetBundle _realBundle;

        public WrappedAssetBundle(AssetBundle realBundle)
        {
            _realBundle = realBundle;
        }

        public bool Contains(string name) => _realBundle.Contains(name);

        public string[] GetAllAssetNames() => _realBundle.GetAllAssetNames();

        public string[] GetAllScenePaths() => _realBundle.GetAllScenePaths();

        public Object Load(string name) => LoadAsset(name);

        public Object LoadAsset(string name) => LoadAsset<Object>(name);

        public T Load<T>(string name) where T : Object => LoadAsset<T>(name);

        public T LoadAsset<T>(string name) where T : Object => _realBundle.LoadAsset<T>(name);

        public Object Load(string name, Type type) => LoadAsset(name, type);

        public Object LoadAsset(string name, Type type) => _realBundle.LoadAsset(name, type);

        public WrappedAssetBundleRequest LoadAssetAsync(string name) => LoadAssetAsync<Object>(name);

        public WrappedAssetBundleRequest LoadAssetAsync<T>(string name) where T : Object =>
            new WrappedAssetBundleRequest(_realBundle.LoadAssetAsync<T>(name));

        public WrappedAssetBundleRequest LoadAssetAsync(string name, Type type) =>
            new WrappedAssetBundleRequest(_realBundle.LoadAssetAsync(name, type));

        public Object[] LoadAll() => LoadAllAssets();

        public Object[] LoadAllAssets() => LoadAllAssets<Object>();

        public T[] LoadAllAssets<T>() where T : Object => _realBundle.LoadAllAssets<T>();

        public Object[] LoadAllAssets(Type type) => _realBundle.LoadAllAssets(type);

        public Object[] LoadAssetWithSubAssets(string name) => LoadAssetWithSubAssets<Object>(name);

        public T[] LoadAssetWithSubAssets<T>(string name) where T : Object => _realBundle.LoadAssetWithSubAssets<T>(name);

        public Object[] LoadAssetWithSubAssets(string name, Type type) => _realBundle.LoadAssetWithSubAssets(name, type);

        public void Unload(bool unloadAllLoadedObjects) => _realBundle.Unload(unloadAllLoadedObjects);
    }
}

/* Might need this if the above fails


public static class Il2CppStringArrayExtensions
{
    public static string[] ToManagedArray(this Il2CppStringArray il2cppStrings)
    {
        string[] managedStrings = new string[il2cppStrings.Length];
        for (int i = 0; i < il2cppStrings.Length; i++)
        {
            managedStrings[i] = il2cppStrings[i];
        }
        return managedStrings;
    }
}

public static class Il2CppObjectArrayExtensions
{
    public static T[] ToManagedArray<T>(this Il2CppReferenceArray<T> il2cppObjects) where T : Object
    {
        T[] managedObjects = new T[il2cppObjects.Length];
        for (int i = 0; i < il2cppObjects.Length; i++)
        {
            managedObjects[i] = il2cppObjects[i];
        }
        return managedObjects;
    }
}
*/


/*
 *
 * [12:57:44.463] [ManorMod] Unhandled exception in coroutine. It will not continue executing.
System.MissingMethodException: Method not found: 'UnityEngine.AssetBundle UnityEngine.AssetBundle.LoadFromStream(System.IO.Stream)'.
   at S1API.AssetBundles.AssetLoader.GetAssetBundleFromStream(String fullResourceName)
   at S1API.AssetBundles.AssetLoader.EasyLoad[T](String bundle_name, String object_name, Assembly assemblyOverride, WrappedAssetBundle& bundle)
   at S1API.AssetBundles.AssetLoader.EasyLoad[T](String bundle_name, String object_name)
   at ManorMod.Core.LoadAssetBundle()+MoveNext()
   at MelonLoader.Support.MonoEnumeratorWrapper.MoveNext() in D:\a\MelonLoader\MelonLoader\Dependencies\SupportModules\Il2Cpp\MonoEnumeratorWrapper.cs:line 39
[12:57:48.792] [ManorMod] System.MissingMethodException: Method not found: 'Void UnityEngine.Events.UnityAction..ctor(System.Object, IntPtr)'.
   at ManorMod.Core.OnLateInitializeMelon()
   at MelonLoader.MelonEvent.<>c.<Invoke>b__1_0(LemonAction x) in D:\a\MelonLoader\MelonLoader\MelonLoader\Melons\Events\MelonEvent.cs:line 174
   at MelonLoader.MelonEventBase`1.Invoke(Action`1 delegateInvoker) in D:\a\MelonLoader\MelonLoader\MelonLoader\Melons\Events\MelonEvent.cs:line 143


*/
