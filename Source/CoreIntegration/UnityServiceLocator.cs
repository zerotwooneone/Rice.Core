using Unity;

namespace CoreIntegration
{
    public class UnityServiceLocator : IServiceLocator
    {
        private readonly IUnityContainer _unityContainer;

        public UnityServiceLocator(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public T Locate<T>() => _unityContainer.Resolve<T>();
    }
}