using System.Collections.Generic;
#if IL2CPPMELON || IL2CPPBEPINEX
using Il2CppScheduleOne.Property;
#elif MONOMELON || MONOBEPINEX
using ScheduleOne.Property;
#endif
namespace S1API.Property
{
    /// <summary>
    /// Provides methods for managing and retrieving property data within the application.
    /// </summary>
    public static class PropertyManager
    {
        /// <summary>
        /// Retrieves a list of all properties available.
        /// </summary>
        /// <returns>A list of <see cref="PropertyWrapper"/> objects representing all available properties.</returns>
        public static List<PropertyWrapper> GetAllProperties()
        {
            var list = new List<PropertyWrapper>();
            foreach (var prop in Il2CppScheduleOne.Property.Property.Properties)
            {
                list.Add(new PropertyWrapper(prop));
            }
            return list;
        }

        /// Retrieves a list of all properties that are currently owned.
        /// <returns>
        /// A list of PropertyWrapper objects representing the owned properties.
        /// </returns>
        public static List<PropertyWrapper> GetOwnedProperties()
        {
            var list = new List<PropertyWrapper>();
            foreach (var prop in Il2CppScheduleOne.Property.Property.OwnedProperties)
            {
                list.Add(new PropertyWrapper(prop));
            }
            return list;
        }

        /// <summary>
        /// Finds a property with the given name from the list of available properties.
        /// </summary>
        /// <param name="name">The name of the property to search for.</param>
        /// <returns>
        /// A <see cref="PropertyWrapper"/> representing the property with the specified name if found; otherwise, null.
        /// </returns>
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