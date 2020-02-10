using System;
using Rice.Core.Abstractions.Compress;
using Rice.Core.Abstractions.File;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Abstractions.Transport;
using Rice.Core.Compress.Gzip;
using Rice.Core.File;
using Rice.Core.ModuleLoad;
using Rice.Core.Transport;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Rice.Core.Unity
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer AddRice(this IUnityContainer unityContainer,
            Func<string, IModuleDependencyLoader> moduleDependencyLoaderFactory)
        {
            unityContainer.RegisterType<ILoadableModuleFactory, LoadableModuleFactory>(new ContainerControlledLifetimeManager(), 
                new InjectionConstructor(moduleDependencyLoaderFactory));
            unityContainer.RegisterSingleton<IModuleLoader, ModuleLoader>();
            
            unityContainer.RegisterSingleton<ITranportableModuleWriter, TransportableModuleIo>();
            unityContainer.RegisterSingleton<ITransportableModuleFactory, TransportableModuleIo>();

            unityContainer.RegisterSingleton<IAssemblyLoader, AssemblyLoader>();

            unityContainer.RegisterSingleton<ICompressor, GzipCompressor>();
            unityContainer.RegisterSingleton<IStreamFactory, FileInfoStreamFactory>();
            
            return unityContainer;
        }
    }
}
