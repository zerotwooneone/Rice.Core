using System;
using System.IO;
using Rice.Core.Abstractions.ModuleLoad;

namespace Rice.Core.ModuleLoad
{
    public class LoadableModuleFactory : ILoadableModuleFactory
    {
        private readonly Func<string, IModuleDependencyLoader> _moduleDependencyLoaderFactory;

        public LoadableModuleFactory(Func<string, IModuleDependencyLoader> moduleDependencyLoaderFactory)
        {
            _moduleDependencyLoaderFactory = moduleDependencyLoaderFactory;
        }
        public ILoadableModule Create(string fullPathToDll, 
            string assemblyName = null)
        {
            if (string.IsNullOrWhiteSpace(fullPathToDll)) throw new ArgumentNullException(nameof(fullPathToDll));
            var dependencyLoader = _moduleDependencyLoaderFactory(fullPathToDll);
            var nameToLoad = string.IsNullOrWhiteSpace(assemblyName) ? Path.GetFileNameWithoutExtension(fullPathToDll) : assemblyName;
            return new LoadableModule(nameToLoad, dependencyLoader);
        }
    }
}