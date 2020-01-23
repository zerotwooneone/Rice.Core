using System;
using System.Linq;
using System.Reflection;
using Rice.Module.Abstractions;

namespace Rice.Core
{
    public class ModuleLoader
    {
        public IModule GetModule(string fullPathToDll)
        {
            //does not load dependencies
            var dll = Assembly.LoadFile(fullPathToDll);

            var exportedTypes = dll.GetExportedTypes();
            var type = exportedTypes.First(FilterModuleType);
            var instance = Activator.CreateInstance(type);
            var module = instance as IModule;

            return module;
        }

        private readonly Type ModuleType = typeof(IModule);
        private bool FilterModuleType(Type t)
        {
            return ModuleType.IsAssignableFrom(t);
        }
    }
}
