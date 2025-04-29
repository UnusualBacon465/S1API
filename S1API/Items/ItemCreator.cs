#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1 = Il2CppScheduleOne;
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
#elif (MONOMELON || MONOBEPINEX)
using S1 = ScheduleOne;
using S1ItemFramework = ScheduleOne.ItemFramework;
#endif

using System.Collections;
using S1API.Products;
using UnityEngine;

namespace S1API.Items
{
    /// <summary>
    /// The item Creator for custom items to be added.
    /// </summary>
    public static class ItemCreator
    {
        /// <summary>
        /// Creates a new ItemDefinition, registers it with the game's Registry, and returns the S1API wrapper.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="category">The category of the item.</param>
        /// <param name="price">The price of the item.</param>
        /// <param name="stackLimit">The stack limit of the item. Default is 10.</param>
        public static ItemDefinition CreateItem(string id, string name, string description, ItemCategory category, int stackLimit = 10)
        {
            // Create a real ScheduleOne.ItemFramework.ItemDefinition
            var newItem = ScriptableObject.CreateInstance<S1ItemFramework.ItemDefinition>();

            // Assign basic fields
            newItem.ID = id;
            newItem.Name = name;
            newItem.Description = description;
            newItem.Category = (S1ItemFramework.EItemCategory)category;
            newItem.StackLimit = stackLimit;

            // Optional: you could assign a default icon if needed.
            newItem.Icon = null; // Set a sprite if you have one.

            // Register with the live Registry
            S1.Registry.Instance.AddToRegistry(newItem);
            
            // Create a Product object and add it to DiscoveredProducts with the specified price
            ((IList)ProductManager.DiscoveredProducts).Add(newItem);
            
            // Return wrapped in S1API.ItemDefinition
            return new ItemDefinition(newItem);
        }
    }
}