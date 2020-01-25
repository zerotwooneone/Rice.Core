namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface ILoadableModule
    {
        string AssemblyName { get; }
        IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}