namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface ILoadableDependency
    {
        string AssemblyName { get; }
        IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}