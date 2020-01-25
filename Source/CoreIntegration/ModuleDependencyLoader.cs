using System.Reflection;
using System.Runtime.Loader;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.ModuleLoad;

namespace CoreIntegration
{
    class ModuleDependencyLoader : IModuleDependencyLoader
    {
        private readonly AssemblyDependencyResolver _assemblyDependencyResolver;

        public ModuleDependencyLoader(string fullPathToDll)
        {
            _assemblyDependencyResolver = new AssemblyDependencyResolver(fullPathToDll);
        }

        public string ResolveAssemblyToPath(AssemblyName assemblyName) =>
            _assemblyDependencyResolver.ResolveAssemblyToPath(assemblyName);
    }
}