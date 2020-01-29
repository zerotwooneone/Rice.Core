using System.Reflection;

namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface IAssemblyLoadContext
    {
        Assembly LoadFromAssemblyName(AssemblyName assemblyName);
    }
}