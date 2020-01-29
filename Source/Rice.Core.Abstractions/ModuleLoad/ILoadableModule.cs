using System.Collections.Generic;

namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface ILoadableModule
    {
        string AssemblyName { get; }
        IModuleDependencyLoader ModuleDependencyLoader { get; }
        IEnumerable<ILoadableDependency> Dependencies { get; }
    }
}