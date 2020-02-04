using ProtoBuf;
using Rice.Core.Abstractions.Serialize;

namespace Rice.Core.Serialize.ProtoBuf
{
    [ProtoContract]
    internal class SerializableDependency : ISerializableDependency
    {
        [ProtoMember(1)]
        public string AssemblyName { get; set; }
        [ProtoMember(2)]
        public byte[] Bytes { get; set; }
    }
}