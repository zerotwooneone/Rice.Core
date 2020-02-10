using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public ILoadableModule Create(string assemblyName, string fullPathToDll)
        {
            if (assemblyName == null) throw new ArgumentNullException(nameof(assemblyName));
            if (string.IsNullOrWhiteSpace(fullPathToDll)) throw new ArgumentNullException(nameof(fullPathToDll));
            var dependencyLoader = _moduleDependencyLoaderFactory(fullPathToDll);
            
            var targetDll = new FileInfo(fullPathToDll);
            var targetAssembly = Assembly.LoadFile(fullPathToDll);
            var targetDllDirectory = targetDll
                .Directory;
            var directoryFiles = targetDllDirectory
                .GetFiles();
            var directoryDlls = directoryFiles
                .Where(f=>f.Extension == ".dll" && f.FullName != targetDll.FullName);
            var loadableDependencies = directoryDlls
                .Select(f=>
                {
                    var assembly = Assembly.LoadFile(f.FullName);
                    return (ILoadableDependency)new LoadableDependency(assembly.GetName().Name, _moduleDependencyLoaderFactory(f.FullName));
                })
                .Where(l=>l.AssemblyName != targetAssembly.GetName().ToString())
                //.Where( ... we should really filter out only those dlls which are referenced by the target)
                ;
            
            
            return new LoadableModule(assemblyName, dependencyLoader, loadableDependencies);
        }
    }
}