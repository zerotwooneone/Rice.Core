using System.Collections.Generic;

namespace Rice.Core.Abstractions.Transport
{
    public interface ITransportableModule
    {
        string AssemblyName { get; }
        byte[] Bytes { get; }

        IEnumerable<ITransportableDependency> Dependencies { get; }
    }
}