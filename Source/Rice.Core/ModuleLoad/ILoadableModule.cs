namespace Rice.Core.ModuleLoad
{
    public interface ILoadableModule
    {
        string FullPathToDll { get; }
        string AssemblyName { get; }
        IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}