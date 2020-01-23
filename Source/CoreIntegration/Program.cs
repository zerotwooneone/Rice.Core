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

            var module = moduleLoader.GetModule(dllFileInfo.FullName);
            
            var executionResult = module.Execute(null);

        }
    }
}
