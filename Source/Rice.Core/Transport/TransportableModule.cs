using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    internal class TransportableModule : ITransportableModule
    {
        public TransportableModule(string assemblyName, byte[] bytes)
        {
            AssemblyName = assemblyName;
            Bytes = bytes;
        }

        public string AssemblyName { get; }
        public byte[] Bytes { get; }
    }
}