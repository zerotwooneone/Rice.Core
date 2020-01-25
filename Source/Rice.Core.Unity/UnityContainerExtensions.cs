using System;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.ModuleLoad;
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

            return unityContainer;
        }
    }
}
