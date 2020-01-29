namespace Rice.Core.Abstractions.Transport
{
    public interface ITransportableDependency
    {
        string AssemblyName { get; }
        byte[] Bytes { get; }
    }
}