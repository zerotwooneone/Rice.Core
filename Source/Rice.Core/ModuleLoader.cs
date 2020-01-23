using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Rice.Module.Abstractions;

namespace Rice.Core
{
    public class ModuleLoader
    {
        public IModule GetModule(Func<AssemblyName, string> resolveAssemblyToPath,
            string fullPathToDll)
        {
            var context = new PluginLoadContext(resolveAssemblyToPath, s=>throw new NotImplementedException("Can not resolve unmanaged dlls"));
            var dll = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(fullPathToDll)));

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
