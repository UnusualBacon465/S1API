using S1API.Internal.Utils;
#if IL2CPP
using S1Product = Il2CppScheduleOne.Product; // or ScheduleOne.Product, depending on the platform
#else
using S1Product = ScheduleOne.Product;
#endif

namespace S1API.Products
{
    public static class ProductDefinitionWrapper
    {
        public static ProductDefinition Wrap(ProductDefinition def)
        {
            var item = def.S1ItemDefinition;

#if IL2CPP
            if (CrossType.Is<S1Product.WeedDefinition>(item, out var weed))
                return new WeedDefinition(weed);
            if (CrossType.Is<S1Product.MethDefinition>(item, out var meth))
                return new MethDefinition(meth);
            if (CrossType.Is<S1Product.CocaineDefinition>(item, out var coke))
                return new CocaineDefinition(coke);
#else
            if (CrossType.Is<S1Product.WeedDefinition>(item, out var weed))
                return new WeedDefinition(weed);
            if (CrossType.Is<S1Product.MethDefinition>(item, out var meth))
                return new MethDefinition(meth);
            if (CrossType.Is<S1Product.CocaineDefinition>(item, out var coke))
                return new CocaineDefinition(coke);
#endif

            return def;
        }
    }
}