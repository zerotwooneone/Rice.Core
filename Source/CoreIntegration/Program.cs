using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Compress;
using Rice.Core.Abstractions.File;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Abstractions.Serialize;
using Rice.Core.Abstractions.Transport;
using Rice.Core.Compress.Gzip;
using Rice.Core.File;
using Rice.Core.Serialize.ProtoBuf;
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

                uc.RegisterSingleton<ICompressor, GzipCompressor>();
                uc.RegisterSingleton<IStreamFactory, FileInfoStreamFactory>();
                uc.RegisterSingleton<ISerializableFactory, ProtoSerializableFactory>();
                uc.RegisterSingleton<ISerializer, SerializerWrapper>();

                return uc;
            }));

            var tests = new[]
            {
                tester.Test("can execute module",
                    CanExecuteModule),
                tester.Test("can read/write module",
                    CanReadWriteModule)
            };

            Task.WhenAll(tests).Wait();
        }

        private const string TestModuleDllPath = "../../../../../Dependencies/TestModule/TestModule.dll";
        private static async Task CanReadWriteModule(ITestContext testContext)
        {
            var serviceLocator = testContext.ServiceLocator;

            var transportableModuleFactory = serviceLocator.Locate<ITransportableModuleFactory>();
            var transportableModule = await LoadFromFile(transportableModuleFactory);

            var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Rice","Rice.Core", $"{DateTime.Now:yyyy.MM.dd.hh.mm.ss}");
            Directory.CreateDirectory(outputPath);
            try
            {
                var writer = serviceLocator.Locate<ITranportableModuleWriter>();
                await writer.WriteToFile(
                    outputPath,
                    transportableModule);
            }
            finally
            {
                var outputDirectory = new DirectoryInfo(outputPath);
                try
                {
                    outputDirectory.Delete();
                }
                catch(Exception ex)
                {
                    // ignored
                }
            }
        }

        private static async Task<ITransportableModule> LoadFromFile(ITransportableModuleFactory reader)
        {
            var dllFileInfo = new FileInfo(TestModuleDllPath);

            var assemblyName = Assembly.LoadFile(dllFileInfo.FullName).GetName();

            var dependencyTuples = FindDependencyStrategies.Default(dllFileInfo.FullName, assemblyName.Name);
            var transportableModule = await reader.Create(dllFileInfo.FullName, assemblyName.ToString(), dependencyTuples);
            return transportableModule;
        }

        private static Task CanExecuteModule(ITestContext testContext)
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
