using System.IO;
using System.Threading.Tasks;
using ProtoBuf;
using Rice.Core.Abstractions.Serialize;

namespace Rice.Core.Serialize.ProtoBuf
{
    public class SerializerWrapper : ISerializer
    {
        public Task<Stream> Serialize(ISerializableModule module)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, module as SerializableModule);
            return Task.FromResult((Stream)stream);
        }

        public Task<ISerializableModule> Deserialize(byte[] input)
        {
            return Task.FromResult((ISerializableModule)Serializer.Deserialize<SerializableModule>(new MemoryStream(input)));
        }
    }
}