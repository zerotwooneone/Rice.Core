using System;
using System.IO;
using System.Runtime.Loader;
using Rice.Core.ModuleLoad;

namespace CoreIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var loadableModuleFactory = new LoadableModuleFactory();

            var dllFileInfo = new FileInfo("../../../../../Dependencies/Rice/TestModule/TestModule.dll");
            var loadableModule = loadableModuleFactory.Create(dllFileInfo.FullName,
                fullPathToDll => new ModuleDependencyLoader(fullPathToDll));

            var moduleLoader = new ModuleLoader();
            var module = moduleLoader.GetModule(loadableModule);
            
            var executionResult = module.Execute(null);

        }
    }
}
