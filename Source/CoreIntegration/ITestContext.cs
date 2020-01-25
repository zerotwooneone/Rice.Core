using Unity;

namespace CoreIntegration
{
    public interface ITestContext
    {
        IServiceLocator ServiceLocator { get; }
    }
}