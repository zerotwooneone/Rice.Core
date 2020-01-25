using System;
using System.IO;
using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class LoadableModuleFactory : ILoadableModuleFactory
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