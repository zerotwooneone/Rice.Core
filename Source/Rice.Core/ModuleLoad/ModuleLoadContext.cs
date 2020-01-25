using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Rice.Core.ModuleLoad
{
    internal class ModuleLoadContext : AssemblyLoadContext
    {
        private readonly IModuleDependencyLoader _moduleDependencyLoader;

        public ModuleLoadContext(IModuleDependencyLoader moduleDependencyLoader)
        {
            _moduleDependencyLoader = moduleDependencyLoader ?? throw new ArgumentNullException(nameof(moduleDependencyLoader));
        }

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