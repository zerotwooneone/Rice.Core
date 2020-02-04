using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
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

            var buffer = await GetFileBytes(dllFileInfo).ConfigureAwait(false);
            var compressed = await CompressBytes(buffer);

            var d = await GetDependencies(dependencies).ConfigureAwait(false);
            var t = new TransportableModule(nameToUse, compressed, d);
                
            return t;
        }

        private async Task<byte[]> CompressBytes(byte[] buffer, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                await new MemoryStream(buffer).CopyToAsync(gzipStream, cancellationToken).ConfigureAwait(false);
            }
            return outputStream.ToArray();
        }

        private async Task<byte[]> DeCompressBytes(byte[] buffer, CancellationToken cancellationToken = default)
        {
            var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress))
            {
                await gzipStream.CopyToAsync(outputStream, cancellationToken).ConfigureAwait(false);
            }
            return outputStream.ToArray();
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

            var buffer = await GetFileBytes(dllFileInfo).ConfigureAwait(false);
            var compressed = await CompressBytes(buffer);
            return new TransportableDependency(nameToUse, compressed);
        }

        private static async Task<byte[]> GetFileBytes(FileInfo dllFileInfo)
        {
            await using var fileStream = dllFileInfo.OpenRead();
            byte[] buffer = new byte[dllFileInfo.Length];
            var count = await fileStream
                .ReadAsync(buffer, 0, (int) dllFileInfo.Length)
                .ConfigureAwait(false);
            return buffer;
        }

        private async Task<IEnumerable<ITransportableDependency>> GetDependencies(IEnumerable<(string fullPath, string assemblyName)> dependencies)
        {
            var tuple = dependencies?.ToArray();
            if (tuple == null || !tuple.Any()) return null;
            var transportableDependencyTasks = tuple.Select(d => CreateDependency(d.fullPath, d.assemblyName));
            var y = await Task.WhenAll(transportableDependencyTasks).ConfigureAwait(false);
            return y;
        }

        public async Task WriteToFile(string fullPath, ITransportableModule module)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                var bytes = await DeCompressBytes(module.Bytes).ConfigureAwait(false);
                await fs.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
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
                        var bytes = await DeCompressBytes(dependency.Bytes).ConfigureAwait(false);
                        await fs.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                    }
                }
            }
            
        }
    }
}
