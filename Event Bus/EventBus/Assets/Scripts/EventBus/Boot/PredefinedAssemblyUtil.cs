using System;
using System.Collections.Generic;
using System.Reflection;

namespace EventBus.Boot
{
    public static class PredefinedAssemblyUtil
    {
        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Dictionary<AssemblyType, Type[]> assemblyTypes = new();
            List<Type> types = new();

            foreach (Assembly assembly in assemblies)
            {
                AssemblyType? assemblyType = GetAssemblyType(assembly.GetName().Name);

                if (assemblyType != null)
                {
                    assemblyTypes.Add((AssemblyType)assemblyType, assembly.GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
            // AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], types, interfaceType);

            return types;
        }

        private static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
        {
            if (assembly == null)
                return;

            foreach (Type type in assembly)
            {
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }
        }
        
        private static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                "Assembly-CSharp-FirstPass" => AssemblyType.AssemblyCSharpFirstPass,
                "Assembly-CSharp-Editor-FirstPass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                _ => null
            };
        }
        
        private enum AssemblyType
        {
            AssemblyCSharp = 0,
            AssemblyCSharpEditor = 1,
            AssemblyCSharpFirstPass = 2,
            AssemblyCSharpEditorFirstPass = 3,
        }
    }
}