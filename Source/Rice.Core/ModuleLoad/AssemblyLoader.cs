using System;
using System.Reflection;
using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public Assembly Load(AssemblyName assemblyName, Func<IAssemblyLoadContext> contextFactory)
        {
            var context = contextFactory();
            var assembly = context.LoadFromAssemblyName(assemblyName);
            return assembly;
        }
    }
}