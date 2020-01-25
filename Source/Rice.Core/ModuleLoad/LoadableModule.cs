namespace Rice.Core.ModuleLoad
{
    internal class LoadableModule : ILoadableModule
    {
        public LoadableModule(string fullPathToDll, string assemblyName, IModuleDependencyLoader moduleDependencyLoader)
        {
            FullPathToDll = fullPathToDll;
            ModuleDependencyLoader = moduleDependencyLoader;
            AssemblyName = assemblyName;
        }

        public string FullPathToDll { get; }
        public string AssemblyName { get; }
        public IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}