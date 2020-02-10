using System;
using System.IO;
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
        public ITestContext Create(string testName)
        {
            var container = _unityContainerFactory();
            var serviceLocator = new UnityServiceLocator(container);
            
            var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Rice","Rice.Core", $"{DateTime.Now:yyyy.MM.dd.hh.mm.ss}");
            var outputDirectory = new DirectoryInfo(Path.Combine(outputPath,testName));
            
            return new TestContext(serviceLocator, outputDirectory);
        }
    }
}