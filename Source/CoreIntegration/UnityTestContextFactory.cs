using System;
using Unity;

namespace CoreIntegration
{
    public class UnityTestContextFactory : ITestContextFactory
    {
        private readonly Func<IUnityContainer> _unityContainerFactory;

        public UnityTestContextFactory(Func<IUnityContainer> unityContainerFactory)
        {
            _unityContainerFactory = unityContainerFactory;
        }
        public ITestContext Create()
        {
            var container = _unityContainerFactory();
            var serviceLocator = new UnityServiceLocator(container);
            return new TestContext(serviceLocator);
        }
    }
}