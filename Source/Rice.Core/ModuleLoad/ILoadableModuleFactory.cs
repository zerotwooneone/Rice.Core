using System;

namespace Rice.Core.ModuleLoad
{
    public interface ILoadableModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPathToDll"></param>
        /// <param name="moduleDependencyLoaderFactory"></param>
        /// <param name="assemblyName">The assembly name, only required if it is different from the file name.</param>
        /// <returns></returns>
        ILoadableModule Create(string fullPathToDll, Func<string, 
            IModuleDependencyLoader> moduleDependencyLoaderFactory,
            string assemblyName = null);
    }
}