using System;
using System.IO;

namespace Rice.Core.ModuleLoad
{
    internal class LoadableModuleFactory : ILoadableModuleFactory
    {
        public ILoadableModule Create(string fullPathToDll, Func<string, 
            IModuleDependencyLoader> moduleDependencyLoaderFactory,
            string assemblyName = null)
        {
            if (moduleDependencyLoaderFactory == null)
                throw new ArgumentNullException(nameof(moduleDependencyLoaderFactory));
            if (string.IsNullOrWhiteSpace(fullPathToDll)) throw new ArgumentNullException(nameof(fullPathToDll));
            var dependencyLoader = moduleDependencyLoaderFactory(fullPathToDll);
            var nameToLoad = string.IsNullOrWhiteSpace(assemblyName) ? Path.GetFileNameWithoutExtension(fullPathToDll) : assemblyName;
            return new LoadableModule(fullPathToDll, nameToLoad, dependencyLoader);
        }
    }
}