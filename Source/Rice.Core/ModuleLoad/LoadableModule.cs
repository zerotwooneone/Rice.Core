using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class LoadableModule : ILoadableModule
    {
        public LoadableModule(string assemblyName, IModuleDependencyLoader moduleDependencyLoader)
        {
            ModuleDependencyLoader = moduleDependencyLoader;
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
        public IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}