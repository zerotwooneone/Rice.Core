using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Compress;
using Rice.Core.Abstractions.File;
using Rice.Core.Abstractions.Serialize;
using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    public class TransportableModuleIo : ITransportableModuleFactory, ITranportableModuleWriter
    {
        private readonly ISerializer _serializer;
        private readonly ICompressor _compressor;
        private readonly ISerializableFactory _serializableFactory;
        private readonly IStreamFactory _fileStreamFactory;
        private const long ArbitraryMaxModuleSize = 50000;

        public TransportableModuleIo(ISerializer serializer, 
            ICompressor compressor,
            ISerializableFactory serializableFactory,
            IStreamFactory fileStreamFactory)
        {
            _serializer = serializer;
            _compressor = compressor;
            _serializableFactory = serializableFactory;
            _fileStreamFactory = fileStreamFactory;
        }

        public async Task<ITransportableModule> Create(string fullPathToDll, 
            string assemblyName,
            IEnumerable<(string fullPath, string assemblyName)> dependencies = null)
        {
            if (fullPathToDll == null) throw new ArgumentNullException(nameof(fullPathToDll));
            var dllFileInfo = new FileInfo(fullPathToDll);
            if(!dllFileInfo.Exists) throw new ArgumentException("File not found");
            if (dllFileInfo.Length > ArbitraryMaxModuleSize) throw new ArgumentException("File size limit exceeded");
            if(string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException(nameof(assemblyName));

            var fileStream = await _fileStreamFactory.OpenRead(fullPathToDll).ConfigureAwait(false);
            
            var serializableDependencies = await GetDependencies(dependencies).ConfigureAwait(false);

            var serializable = await _serializableFactory.CreateModule(assemblyName, fileStream, serializableDependencies).ConfigureAwait(false);

            var serialized = await _serializer.Serialize(serializable).ConfigureAwait(false);

            var compressed = await _compressor.Compress(serialized).ConfigureAwait(false);

            var memoryStream = new MemoryStream();
            await using (memoryStream.ConfigureAwait(false))
            {
                await compressed.CopyToAsync(memoryStream).ConfigureAwait(false);
                return new TransportableModule(memoryStream.ToArray());
            }
        }

        private async Task<IEnumerable<ISerializableDependency>> GetDependencies(IEnumerable<(string fullPath, string assemblyName)> dependencies)
        {
            var tuple = dependencies?.ToArray();
            if (tuple == null || !tuple.Any()) return null;
            var transportableDependencyTasks = tuple.Select(async d =>
            {
                var stream = await _fileStreamFactory.OpenRead(d.fullPath).ConfigureAwait(false);
                return _serializableFactory.CreateDependency(d.assemblyName, stream);
            })
                .Select(t=>t.Unwrap());
            var y = await Task.WhenAll(transportableDependencyTasks).ConfigureAwait(false);
            return y;
        }

        public async Task WriteToFile(string fullPath, ITransportableModule module)
        {
            var compressed = new MemoryStream(module.CompressedAssemblies);
            var decompressed = await _compressor.Decompress(compressed);
            var serializable = await _serializer.Deserialize(decompressed);

            var dependencies = (serializable.Dependencies) ?? Enumerable.Empty<ISerializableDependency>();
            var x = new[] {(assemblyName: serializable.AssemblyName, bytes: serializable.Bytes)}
                .Concat(dependencies.Select(d =>
                    (assemblyName: d.AssemblyName, bytes: d.Bytes)));

            var tasks = x.Select(async t =>
            {
                var path = Path.Combine(fullPath, $"{t.assemblyName}.dll");
                var outputStream = await _fileStreamFactory.CreateOrOverwrite(path).ConfigureAwait(false);
                await using (outputStream)
                {
                    await outputStream.WriteAsync(t.bytes);
                }
            });
            await Task.WhenAll(tasks);
        }
    }
}
