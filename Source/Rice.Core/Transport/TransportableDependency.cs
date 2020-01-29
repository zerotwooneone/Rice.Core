using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    internal class TransportableDependency : ITransportableDependency
    {
        public TransportableDependency(string assemblyName, 
            byte[] bytes)
        {
            AssemblyName = assemblyName;
            Bytes = bytes;
        }

        public string AssemblyName { get; }
        public byte[] Bytes { get; }
    }
}