#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1Growing = Il2CppScheduleOne.Growing;
using ScheduleOneRegistry = Il2CppScheduleOne.Registry;
#elif (MONOMELON || MONOBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1Growing = ScheduleOne.Growing;
using ScheduleOneRegistry = ScheduleOne.Registry;
#endif

using S1API.Items;
using UnityEngine;

namespace S1API.Growing
{
    /// <summary>
    /// API for creating and cloning seeds easily.
    /// </summary>
    public static class SeedCreator
    {
        /// <summary>
        /// Creates a brand new SeedDefinition and registers it.
        /// </summary>
        public static SeedDefinition CreateSeed(string id, string name, string description, int stackLimit,
            S1Growing.FunctionalSeed? functionSeedPrefab, S1Growing.Plant? plantPrefab, Sprite? icon = null)
        {
            var newSeed = ScriptableObject.CreateInstance<S1Growing.SeedDefinition>();
            newSeed.ID = id;
            newSeed.Name = name;
            newSeed.Description = description;
            newSeed.StackLimit = stackLimit;
            newSeed.FunctionSeedPrefab = functionSeedPrefab;
            newSeed.PlantPrefab = plantPrefab;
            newSeed.Category = S1ItemFramework.EItemCategory.Growing;
            newSeed.Icon = icon;

            ScheduleOneRegistry.Instance.AddToRegistry(newSeed);
            return new SeedDefinition(newSeed);
        }

        /// <summary>
        /// Clones an existing SeedDefinition and registers it.
        /// </summary>
        public static SeedDefinition CloneSeed(string existingSeedId, string newId, string newName, string newDescription)
        {
            var baseSeed = ScheduleOneRegistry.GetItem<S1Growing.SeedDefinition>(existingSeedId);
            if (baseSeed == null)
                throw new System.Exception($"Could not find base SeedDefinition with ID '{existingSeedId}'!");

            var newSeed = ScriptableObject.Instantiate(baseSeed);
            newSeed.ID = newId;
            newSeed.Name = newName;
            newSeed.Description = newDescription;

            ScheduleOneRegistry.Instance.AddToRegistry(newSeed);
            return new SeedDefinition(newSeed);
        }
    }
}
