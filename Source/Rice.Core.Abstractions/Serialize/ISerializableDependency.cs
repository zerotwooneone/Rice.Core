namespace Rice.Core.Abstractions.Serialize
{
    public interface ISerializableDependency
    {
        string AssemblyName { get; }
        byte[] Bytes { get; }
    }
}