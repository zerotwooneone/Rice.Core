using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    public class TransportableModuleIo : ITransportableModuleFactory, ITranportableModuleWriter
    {
        private const long ArbitraryMaxModuleSize = 50000;

        public async Task<ITransportableModule> Create(string fullPathToDll, 
            string assemblyName,
            IEnumerable<(string fullPath, string assemblyName)> dependencies = null)
        {
            if (fullPathToDll == null) throw new ArgumentNullException(nameof(fullPathToDll));
            var dllFileInfo = new FileInfo(fullPathToDll);
            if(!dllFileInfo.Exists) throw new ArgumentException("File not found");
            if (dllFileInfo.Length > ArbitraryMaxModuleSize) throw new ArgumentException("File size limit exceeded");

            var nameToUse = string.IsNullOrWhiteSpace(assemblyName)
                ? Path.GetFileNameWithoutExtension(dllFileInfo.Name)
                : assemblyName;
            
            using (var fileStream = dllFileInfo.OpenRead())
            {
                byte[] buffer = new byte[dllFileInfo.Length];
                var count = await fileStream
                    .ReadAsync(buffer, 0, (int) dllFileInfo.Length)
                    .ConfigureAwait(false);
                var d = await GetDependencies(dependencies);
                return new TransportableModule(nameToUse, buffer, d);
            }
        }

        // need to make this a separate factory class
        public async Task<ITransportableDependency> CreateDependency(string fullPathToDll, 
            string assemblyName)
        {
            if (fullPathToDll == null) throw new ArgumentNullException(nameof(fullPathToDll));
            var dllFileInfo = new FileInfo(fullPathToDll);
            if(!dllFileInfo.Exists) throw new ArgumentException("File not found");
            if (dllFileInfo.Length > ArbitraryMaxModuleSize) throw new ArgumentException("File size limit exceeded");

            var nameToUse = string.IsNullOrWhiteSpace(assemblyName)
                ? Path.GetFileNameWithoutExtension(dllFileInfo.Name)
                : assemblyName;
            
            using (var fileStream = dllFileInfo.OpenRead())
            {
                byte[] buffer = new byte[dllFileInfo.Length];
                var count = await fileStream
                    .ReadAsync(buffer, 0, (int) dllFileInfo.Length)
                    .ConfigureAwait(false);
                return new TransportableDependency(nameToUse, buffer);
            }
        }

        private async Task<IEnumerable<ITransportableDependency>> GetDependencies(IEnumerable<(string fullPath, string assemblyName)> dependencies)
        {
            var tuple = dependencies?.ToArray();
            if (tuple == null || !tuple.Any()) return null;
            var transportableDependencyTasks = tuple.Select(d => CreateDependency(d.fullPath, d.assemblyName));
            var y = await Task.WhenAll(transportableDependencyTasks);
            return y;
        }

        public async Task WriteToFile(string fullPath, ITransportableModule module)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await fs.WriteAsync(module.Bytes, 0, module.Bytes.Length);
            }

            var dependencies = module.Dependencies?.ToArray() ?? new ITransportableDependency[0];
            if (dependencies.Any())
            {
                var directory = new FileInfo(fullPath).Directory;
                foreach (var dependency in dependencies)
                {
                    var path = Path.Combine(directory.FullName, $"{dependency.AssemblyName}.dll");
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        await fs.WriteAsync(dependency.Bytes, 0, dependency.Bytes.Length);
                    }
                }
            }
            
        }
    }
}
