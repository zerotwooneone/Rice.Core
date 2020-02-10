using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    internal class TransportableModule : ITransportableModule
    {
        public TransportableModule(string assemblyName, 
            byte[] compressedAssemblies)
        {
            CompressedAssemblies = compressedAssemblies;
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
        public byte[] CompressedAssemblies { get; }
    }
}