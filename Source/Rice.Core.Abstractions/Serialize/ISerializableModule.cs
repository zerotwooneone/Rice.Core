using System.Collections.Generic;

namespace Rice.Core.Abstractions.Serialize
{
    public interface ISerializableModule
    {
        string AssemblyName { get; }
        byte[] Bytes { get; }

        IEnumerable<ISerializableDependency> Dependencies { get; }
    }
}