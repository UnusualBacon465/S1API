using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if (IL2CPP)
// using Il2CppInterop.Runtime;
// using Il2CppSystem;
// using Il2CppSystem.Collections.Generic;
#elif (MONO)
#endif

namespace S1API.Internal.Utils
{
    /// <summary>
    /// INTERNAL: Provides generic reflection based methods for easier API development
    /// </summary>
    internal static class ReflectionUtils
    {
        /// <summary>
        /// Identifies all classes derived from another class.
        /// </summary>
        /// <typeparam name="TBaseClass">The base class derived from.</typeparam>
        /// <returns>A list of all types derived from the base class.</returns>
        internal static List<Type> GetDerivedClasses<TBaseClass>()
        {
            List<Type> derivedClasses = new List<Type>();
            Assembly[] applicableAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName.StartsWith("System") &&
                        !assembly.FullName.StartsWith("Unity") &&
                        !assembly.FullName.StartsWith("Il2Cpp") &&
                        !assembly.FullName.StartsWith("mscorlib") &&
                        !assembly.FullName.StartsWith("Mono.") &&
                        !assembly.FullName.StartsWith("netstandard"))
                .ToArray();
            foreach (Assembly assembly in applicableAssemblies)
                derivedClasses.AddRange(assembly.GetTypes()
                    .Where(type => typeof(TBaseClass).IsAssignableFrom(type) 
                                   && type != typeof(TBaseClass) 
                                   && !type.IsAbstract));
            
            return derivedClasses;
        }

        /// <summary>
        /// Gets all types by their name.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>The actual type identified by the name.</returns>
        public static Type? GetTypeByName(string typeName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type? foundType = assembly.GetTypes().FirstOrDefault(type => type.Name == typeName);
                if (foundType == null)
                    continue;
                
                return foundType;
            }

            return null;
        }
    }
}