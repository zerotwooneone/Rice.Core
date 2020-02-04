using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    internal class TransportableModule : ITransportableModule
    {
        public TransportableModule(byte[] compressedAssemblies)
        {
            CompressedAssemblies = compressedAssemblies;
        }

        public byte[] CompressedAssemblies { get; }
    }
}