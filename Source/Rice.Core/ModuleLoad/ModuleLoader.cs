using System;
using System.Linq;
using System.Reflection;
using Rice.Module.Abstractions;

namespace Rice.Core.ModuleLoad
{
    internal class ModuleLoader : IModuleLoader
    {
        public IModule GetModule(ILoadableModule loadableModule)
        {
            var context = new ModuleLoadContext(loadableModule.ModuleDependencyLoader);
            var assemblyName = new AssemblyName(loadableModule.AssemblyName ?? throw new InvalidOperationException());
            var dll = context.LoadFromAssemblyName(assemblyName);

            var exportedTypes = dll.GetExportedTypes();

            var type = exportedTypes.First(FilterModuleType); 

            var instance = Activator.CreateInstance(type);
            var module = instance as IModule;

            return module;
        }

        private readonly Type _moduleType = typeof(IModule);
        private bool FilterModuleType(Type t)
        {
            return _moduleType.IsAssignableFrom(t);
        }
    }
}
