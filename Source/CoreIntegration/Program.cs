using System;
using System.IO;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Unity;
using Unity;

namespace CoreIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var uc = new UnityContainer();
            uc.AddRice(fullPathToDll => new ModuleDependencyLoader(fullPathToDll));

            var loadableModuleFactory = uc.Resolve<ILoadableModuleFactory>();

            var dllFileInfo = new FileInfo("../../../../../Dependencies/Rice/TestModule/TestModule.dll");
            var loadableModule = loadableModuleFactory.Create(dllFileInfo.FullName,
                fullPathToDll => new ModuleDependencyLoader(fullPathToDll));

            var moduleLoader = uc.Resolve<IModuleLoader>();
            var module = moduleLoader.GetModule(loadableModule);
            
            var executionResult = module.Execute(null);

        }
    }
}
