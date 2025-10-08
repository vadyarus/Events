using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace VadyaRus.Events {
    internal static class PredefinedAssemblyUtility {
        /// <summary>
        /// Method looks through a given assembly and adds types that fulfill a certain interface to the provided collection.
        /// </summary>
        /// <param name="assemblyTypes">Array of Type objects representing all the types in the assembly.</param>
        /// <param name="interfaceType">Type representing the interface to be checked against.</param>
        /// <param name="results">Collection of types where result should be added.</param>
        static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results) {
            if (assemblyTypes == null) return;
            for (int i = 0; i < assemblyTypes.Length; i++) {
                Type type = assemblyTypes[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type)) {
                    results.Add(type);
                }
            }
        }

        /// <summary>
        /// Gets all Types from all assemblies in the current AppDomain that implement the provided interface type.
        /// </summary>
        /// <param name="interfaceType">Interface type to get all the Types for.</param>
        /// <returns>List of Types implementing the provided interface type.</returns>    
        public static List<Type> GetTypes(Type interfaceType) {
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies) {
                try {
                    var assemblyTypes = assembly.GetTypes();
                    AddTypesFromAssembly(assemblyTypes, interfaceType, types);
                }
                catch (ReflectionTypeLoadException) {

                    // This can happen with certain assemblies, it's safe to ignore.
                    Debug.LogWarning($"Could not load types from assembly: {assembly.FullName}");
                }
            }
            return types;
        }
    }
}