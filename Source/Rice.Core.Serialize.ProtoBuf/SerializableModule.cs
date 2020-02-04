using System.Collections.Generic;
using ProtoBuf;
using Rice.Core.Abstractions.Serialize;

namespace Rice.Core.Serialize.ProtoBuf
{
    [ProtoContract]
    internal class SerializableModule : ISerializableModule
    {
        [ProtoMember(1)]
        public string AssemblyName { get; set; }
        [ProtoMember(2)]
        public byte[] Bytes { get; set; }
        [ProtoMember(3)]
        public IEnumerable<SerializableDependency> Dependencies { get; set; }
        IEnumerable<ISerializableDependency> ISerializableModule.Dependencies => Dependencies;
    }
}