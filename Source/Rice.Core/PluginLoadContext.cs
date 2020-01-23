using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Rice.Core
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly Func<AssemblyName,string> _resolveAssemblyToPath;
        private readonly Func<string, string> _resolveUnmanagedDllToPath;

        public PluginLoadContext(Func<AssemblyName, string> resolveAssemblyToPath,
            Func<string,string> resolveUnmanagedDllToPath)
        {
            _resolveAssemblyToPath = resolveAssemblyToPath;
            _resolveUnmanagedDllToPath = resolveUnmanagedDllToPath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}