namespace CoreIntegration
{
    public class TestContext : ITestContext
    {
        public TestContext(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
        public IServiceLocator ServiceLocator { get; }
    }
}