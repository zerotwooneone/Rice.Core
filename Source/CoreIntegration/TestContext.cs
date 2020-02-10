using System;
using System.IO;

namespace CoreIntegration
{
    public class TestContext : ITestContext
    {
        public TestContext(IServiceLocator serviceLocator, 
            DirectoryInfo outputDirectory)
        {
            ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
            OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
        }
        public IServiceLocator ServiceLocator { get; }
        public DirectoryInfo OutputDirectory { get; }

        public void Dispose()
        {
            try
            {
                //OutputDirectory.Refresh();
                //if (OutputDirectory.Exists)
                //{
                //    OutputDirectory.Delete(true);
                //}
            }
            catch (Exception )
            {
                //nothing
            }
        }
    }
}