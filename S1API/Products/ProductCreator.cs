#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1ItemFramework = Il2CppScheduleOne.ItemFramework;
using S1Product = Il2CppScheduleOne.Product;
using ScheduleOneRegistry = Il2CppScheduleOne.Registry;
#elif (MONOMELON || MONOBEPINEX)
using S1ItemFramework = ScheduleOne.ItemFramework;
using S1Product = ScheduleOne.Product;
using ScheduleOneRegistry = ScheduleOne.Registry;
#endif

using S1API.Items;
using UnityEngine;

namespace S1API.Products
{
    /// <summary>
    /// API for creating and cloning ProductDefinitions easily.
    /// </summary>
    public static class ProductCreator
    {
        /// <summary>
        /// Creates a brand new ProductDefinition and registers it.
        /// </summary>
        public static ProductDefinition CreateProduct(string id, string name, string description, int stackLimit, float basePrice, Sprite? icon = null)
        {
            var newProduct = ScriptableObject.CreateInstance<S1Product.ProductDefinition>();
            newProduct.ID = id;
            newProduct.Name = name;
            newProduct.Description = description;
            newProduct.StackLimit = stackLimit;
            newProduct.BasePrice = basePrice;
            newProduct.Category = S1ItemFramework.EItemCategory.Product;
            newProduct.Icon = icon;

            ScheduleOneRegistry.Instance.AddToRegistry(newProduct);
            return new ProductDefinition(newProduct);
        }


        /// <summary>
        /// Clones an existing ProductDefinition and registers it.
        /// </summary>
        public static ProductDefinition CloneProduct(string existingProductId, string newId, string newName, string newDescription, float newPrice = -1f, Sprite? newIcon = null)
        {
            var baseProduct = ScheduleOneRegistry.GetItem<S1Product.ProductDefinition>(existingProductId);
            if (baseProduct == null)
                throw new System.Exception($"Could not find base ProductDefinition with ID '{existingProductId}'!");

            var newProduct = ScriptableObject.Instantiate(baseProduct);
            newProduct.ID = newId;
            newProduct.Name = newName;
            newProduct.Description = newDescription;

            if (newPrice >= 0)
                newProduct.BasePrice = newPrice;

            if (newIcon != null)
                newProduct.Icon = newIcon;

            ScheduleOneRegistry.Instance.AddToRegistry(newProduct);
            return new ProductDefinition(newProduct);
        }
    }
}
