using System;
using System.Reflection;
using System.Runtime.Loader;
using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class ModuleLoadContext : AssemblyLoadContext, IAssemblyLoadContext
    {
        private readonly IModuleDependencyLoader _moduleDependencyLoader;

        public ModuleLoadContext(IModuleDependencyLoader moduleDependencyLoader)
        {
            _moduleDependencyLoader = moduleDependencyLoader ?? throw new ArgumentNullException(nameof(moduleDependencyLoader));
        }

        Assembly IAssemblyLoadContext.LoadFromAssemblyName(AssemblyName assemblyName) => Load(assemblyName);
        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _moduleDependencyLoader.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            throw new NotImplementedException("Loading unmanaged libraries is not supported");
            //string libraryPath = _resolveUnmanagedDllToPath(unmanagedDllName);
            //if (libraryPath != null)
            //{
            //    return LoadUnmanagedDllFromPath(libraryPath);
            //}
            //return IntPtr.Zero;
        }

        protected new IntPtr LoadUnmanagedDllFromPath(string path)
        {
            throw new NotImplementedException("Loading unmanaged libraries is not supported");
        }
    }
}