using System;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.ModuleLoad;
using Unity;

namespace Rice.Core.Unity
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer AddRice(this IUnityContainer unityContainer,
            Func<string, IModuleDependencyLoader> moduleDependencyLoaderFactory)
        {
            unityContainer.RegisterSingleton<ILoadableModuleFactory, LoadableModuleFactory>();
            unityContainer.RegisterSingleton<IModuleLoader, ModuleLoader>();

            return unityContainer;
        }
    }
}
