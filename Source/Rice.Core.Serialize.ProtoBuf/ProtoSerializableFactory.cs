using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Serialize;

namespace Rice.Core.Serialize.ProtoBuf
{
    public class ProtoSerializableFactory : ISerializableFactory
    {
        public async Task<ISerializableModule> CreateModule(string assemblyName, 
            Stream assemblyStream,
            IEnumerable<ISerializableDependency> dependencies)
        {
            byte[] bytes;
            var stream = new MemoryStream();
            await using (stream.ConfigureAwait(false))
            {
                await assemblyStream.CopyToAsync(stream).ConfigureAwait(false);
                bytes = stream.ToArray();
            }

            SerializableDependency Convert(ISerializableDependency d)
            {
                return (d as SerializableDependency) ?? new SerializableDependency
                {
                    AssemblyName = d.AssemblyName,
                    Bytes = d.Bytes
                };
            }

            return new SerializableModule{AssemblyName = assemblyName, Bytes = bytes, Dependencies = dependencies.Select(Convert)};
        }

        public async Task<ISerializableDependency> CreateDependency(string assemblyName, Stream assemblyStream)
        {
            byte[] bytes;
            var stream = new MemoryStream();
            await using (stream.ConfigureAwait(false))
            {
                await assemblyStream.CopyToAsync(stream).ConfigureAwait(false);
                bytes = stream.ToArray();
            }
            return new SerializableDependency{AssemblyName = assemblyName, Bytes = bytes};
        }
    }
}