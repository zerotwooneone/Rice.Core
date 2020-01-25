using System.Reflection;

namespace Rice.Core.ModuleLoad
{
    /// <summary>
    /// An interface which must be provided in the host application. Most implementations will simply want to reference System.Runtime.Loader.AssemblyDependencyResolver
    /// </summary>
    public interface IModuleDependencyLoader
    {
        /// <summary>
        /// Returns the path to the assembly. Most implementations will simply return System.Runtime.Loader.AssemblyDependencyResolver.ResolveAssemblyToPath
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        string ResolveAssemblyToPath(AssemblyName assemblyName);
    }
}