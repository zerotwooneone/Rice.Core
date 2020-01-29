using System.Collections.Generic;
using System.Linq;
using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    internal class TransportableModule : ITransportableModule
    {
        public TransportableModule(string assemblyName, 
            byte[] bytes, 
            IEnumerable<ITransportableDependency> dependencies = null)
        {
            AssemblyName = assemblyName;
            Bytes = bytes;
            Dependencies = dependencies?.ToArray();
        }

        public string AssemblyName { get; }
        public byte[] Bytes { get; }
        public IEnumerable<ITransportableDependency> Dependencies { get; }
    }
}