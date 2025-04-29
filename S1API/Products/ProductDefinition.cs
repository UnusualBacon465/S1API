#if (IL2CPPMELON || IL2CPPBEPINEX)
using Il2CppInterop.Runtime.InteropTypes;
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX)
using S1Product = ScheduleOne.Product;
#endif

using S1API.Internal.Utils;
using S1API.Items;
using UnityEngine;

namespace S1API.Products
{
    /// <summary>
    /// Represents a product definition in the game.
    /// </summary>
    public class ProductDefinition : ItemDefinition
    {
        /// <summary>
        /// INTERNAL: Stored reference to the game product definition.
        /// </summary>
        internal S1Product.ProductDefinition S1ProductDefinition => 
            CrossType.As<S1Product.ProductDefinition>(S1ItemDefinition);
        
        /// <summary>
        /// INTERNAL: Creates a product definition from the in-game product definition.
        /// </summary>
        /// <param name="productDefinition">The game product definition to wrap.</param>
        internal ProductDefinition(S1Product.ProductDefinition productDefinition)
            : base(productDefinition) { }

        /// <summary>
        /// The base price associated with this product.
        /// This can be set to influence the price of the product.
        /// </summary>
        public float BasePrice
        {
            get => S1ProductDefinition.BasePrice;
            set => S1ProductDefinition.BasePrice = value;
        }

        /// <summary>
        /// The calculated market price of the product (read-only).
        /// </summary>
        public float Price
        {
            get => S1ProductDefinition.Price;
            set => throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the icon associated with this product.
        /// </summary>
        public Sprite Icon
        {
            get => S1ProductDefinition.Icon;
            set => S1ProductDefinition.Icon = value;
        }

        /// <summary>
        /// Creates an instance of this product in-game.
        /// </summary>
        /// <param name="quantity">The quantity of product.</param>
        /// <returns>An instance of the product.</returns>
        public override ItemInstance CreateInstance(int quantity = 1) =>
            new ProductInstance(CrossType.As<S1Product.ProductItemInstance>(
                S1ProductDefinition.GetDefaultInstance(quantity)));
    }
}
