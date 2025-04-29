#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1Product = Il2CppScheduleOne.Product;
#elif (MONOMELON || MONOBEPINEX)
using S1Product = ScheduleOne.Product;
#endif
/// <summary>
/// ProductUtils all utils needed for the creator and so on.
/// </summary>
public static class ProductUtils
{
    
    
    /// <summary>
    /// Adds products to the product manager.
    /// </summary>
    public static void AddToProductManager(S1Product.ProductDefinition product)
    {
        var productManager = UnityEngine.Object.FindObjectOfType<S1Product.ProductManager>();
        if (productManager == null)
        {
            throw new System.Exception("Could not find ProductManager in the scene!");
        }

        if (!productManager.AllProducts.Contains(product))
        {
            productManager.AllProducts.Add(product);
        }
    }
}