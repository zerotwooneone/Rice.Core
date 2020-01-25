using System;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Abstractions.Transport;
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
            unityContainer.RegisterType<ILoadableModuleFactory, LoadableModuleFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor(moduleDependencyLoaderFactory));
            unityContainer.RegisterSingleton<IModuleLoader, ModuleLoader>();
            
            unityContainer.RegisterSingleton<ITranportableModuleWriter, TransportableTranportableModuleIo>();
            unityContainer.RegisterSingleton<ITransportableModuleFactory, TransportableTranportableModuleIo>();

            return unityContainer;
        }
    }
}
