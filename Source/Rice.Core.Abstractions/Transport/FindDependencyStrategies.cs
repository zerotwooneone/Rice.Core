using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rice.Core.Abstractions.Transport
{
    public static class FindDependencyStrategies
    {
        public static readonly FindDependencyStrategy Default = GetAllDllsFromTargetDirectory;

        public static IEnumerable<(string fullPath, string assemblyName)> GetAllDllsFromTargetDirectory(
            string fullPathToDll,
            string assemblyName = null)
        {
            var targetDll = new FileInfo(fullPathToDll);
            var targetAssembly = Assembly.LoadFile(fullPathToDll);
            var targetDllDirectory = targetDll
                .Directory;
            var directoryFiles = targetDllDirectory
                .GetFiles();
            var directoryDlls = directoryFiles
                .Where(f=>f.Extension == ".dll" && f.FullName != targetDll.FullName);
            var loadableDependencies = directoryDlls
                    .Select(f=>
                    {
                        var assembly = Assembly.LoadFile(f.FullName);
                        return (FullName:f.FullName,AssemblyName: assembly.GetName().Name);
                    })
                    .Where(l=>l.AssemblyName != targetAssembly.GetName().ToString())
                //.Where( ... we should really filter out only those dlls which are referenced by the target)
                ;
            return loadableDependencies;
        }
    }
}