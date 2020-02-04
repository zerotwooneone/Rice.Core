namespace Rice.Core.Abstractions.Transport
{
    public interface ITransportableModule
    {
        byte[] CompressedAssemblies { get; }
    }
}