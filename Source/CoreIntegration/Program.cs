using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Abstractions.Transport;
using Rice.Core.Unity;
using Unity;

namespace CoreIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var tester = new Tester(new UnityTestContextFactory(() =>
            {
                var uc = new UnityContainer();
                uc.AddRice(fullPathToDll => new ModuleDependencyLoader(fullPathToDll));
                return uc;
            }));

            var tests = new[]
            {
                tester.Test("can create module",
                    CanCreateModule),
                tester.Test("can read/write module",
                    CanReadWriteModule)
            };

            Task.WhenAll(tests).Wait();
        }

        private const string TestModuleDllPath = "../../../../../Dependencies/TestModule/TestModule.dll";
        private static async Task CanReadWriteModule(ITestContext testContext)
        {
            var serviceLocator = testContext.ServiceLocator;

            var dllFileInfo = new FileInfo(TestModuleDllPath);

            var reader = serviceLocator.Locate<ITransportableModuleFactory>();
            var transportableModule = await reader.Create(dllFileInfo.FullName);

            var tempFileName =$"{DateTime.Now.ToString("yyyyMMdd.HHmmss.ffff")}.dll" ;
            var tempFilePath = Path.Combine(Directory.GetParent(TestModuleDllPath).FullName, tempFileName);

            var writer = serviceLocator.Locate<ITranportableModuleWriter>();
            await writer.WriteToFile(
                tempFilePath,
                transportableModule);

            try
            {
                var assemblyName = Path.GetFileNameWithoutExtension(TestModuleDllPath);
                var result = await reader.Create(tempFilePath, assemblyName);
                
                var loadableModuleFactory = serviceLocator.Locate<ILoadableModuleFactory>();

                var loadableModule = loadableModuleFactory.Create(tempFilePath, result.AssemblyName);

                var moduleLoader = serviceLocator.Locate<IModuleLoader>();
                var module = moduleLoader.GetModule(loadableModule);
            
                var executionResult = module.Execute(null);
            }
            finally
            {
                var tempFile = new FileInfo(tempFilePath);
                try
                {
                    tempFile.Delete();
                }
                catch(Exception ex)
                {
                    // ignored
                }
            }
        }

        private static Task CanCreateModule(ITestContext testContext)
        {
            var serviceLocator = testContext.ServiceLocator;
            var loadableModuleFactory = serviceLocator.Locate<ILoadableModuleFactory>();

            var dllFileInfo = new FileInfo(TestModuleDllPath);
            var loadableModule = loadableModuleFactory.Create(dllFileInfo.FullName);

            var moduleLoader = serviceLocator.Locate<IModuleLoader>();
            var module = moduleLoader.GetModule(loadableModule);
            
            var executionResult = module.Execute(null);

            return Task.CompletedTask;
        }
    }
}
