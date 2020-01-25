namespace Rice.Core.Abstractions.Transport
{
    public interface ITransportableModule
    {
        string AssemblyName { get; }
        byte[] Bytes { get; }
    }
}