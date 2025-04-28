using System.Collections.Generic;
#if IL2CPPMELON || IL2CPPBEPINEX
using Il2CppScheduleOne.Property;
#elif MONOMELON || MONOBEPINEX
using ScheduleOne.Property;
#endif
namespace S1API.Property
{
    public static class PropertyManager
    {
        public static List<PropertyWrapper> GetAllProperties()
        {
            var list = new List<PropertyWrapper>();
            foreach (var prop in Il2CppScheduleOne.Property.Property.Properties)
            {
                list.Add(new PropertyWrapper(prop));
            }
            return list;
        }

        public static List<PropertyWrapper> GetOwnedProperties()
        {
            var list = new List<PropertyWrapper>();
            foreach (var prop in Il2CppScheduleOne.Property.Property.OwnedProperties)
            {
                list.Add(new PropertyWrapper(prop));
            }
            return list;
        }

        public static PropertyWrapper FindPropertyByName(string name)
        {
            foreach (var prop in Il2CppScheduleOne.Property.Property.Properties)
            {
                if (prop.PropertyName == name)
                {
                    return new PropertyWrapper(prop);
                }
            }
            return null;
        }
    }
}