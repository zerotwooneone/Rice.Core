using System;
using System.Linq;
using System.Reflection;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Module.Abstractions;

namespace Rice.Core.ModuleLoad
{
    public class ModuleLoader : IModuleLoader
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly Type _moduleType = typeof(IModule);

        internal ModuleLoader(IAssemblyLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }
        public IModule GetModule(ILoadableModule loadableModule)
        {
            if (loadableModule == null) throw new ArgumentNullException(nameof(loadableModule));
            var assemblyName = new AssemblyName(loadableModule.AssemblyName ?? throw new InvalidOperationException());
            var assembly = _assemblyLoader.Load(assemblyName, ()=>new ModuleLoadContext(loadableModule.ModuleDependencyLoader));

            var exportedTypes = assembly.GetExportedTypes();

            var type = exportedTypes.First(FilterModuleType); 

            var instance = Activator.CreateInstance(type);
            var module = instance as IModule;

            return module;
        }

        private bool FilterModuleType(Type t)
        {
            return _moduleType.IsAssignableFrom(t);
        }
    }
}
