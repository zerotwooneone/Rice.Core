using System;
using System.IO;
using Rice.Core;

namespace CoreIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dllFileInfo = new FileInfo("../../../../../Dependencies/Rice/TestModule/TestModule.dll");

            var moduleLoader = new ModuleLoader();

            var adr = new System.Runtime.Loader.AssemblyDependencyResolver(dllFileInfo.FullName);

            var module = moduleLoader.GetModule(adr.ResolveAssemblyToPath,dllFileInfo.FullName);
            
            var executionResult = module.Execute(null);

        }
    }
}
