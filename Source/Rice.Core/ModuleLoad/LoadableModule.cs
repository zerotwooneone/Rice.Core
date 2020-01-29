using System;
using System.Collections.Generic;
using System.Linq;
using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class LoadableModule : ILoadableModule
    {
        public LoadableModule(string assemblyName, 
            IModuleDependencyLoader moduleDependencyLoader, 
            IEnumerable<ILoadableDependency> dependencies = null)
        {
            if(string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Assembly name cannot be null or blank", nameof(assemblyName));
            ModuleDependencyLoader = moduleDependencyLoader ?? throw new ArgumentNullException(nameof(moduleDependencyLoader));
            Dependencies = dependencies?.ToArray();
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
        public IModuleDependencyLoader ModuleDependencyLoader { get; }
        public IEnumerable<ILoadableDependency> Dependencies { get; }
    }

    public class LoadableDependency : ILoadableDependency
    {
        public LoadableDependency(string assemblyName, IModuleDependencyLoader moduleDependencyLoader)
        {
            ModuleDependencyLoader = moduleDependencyLoader;
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
        public IModuleDependencyLoader ModuleDependencyLoader { get; }
    }
}