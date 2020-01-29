using System;
using System.Reflection;

namespace Rice.Core.Abstractions.ModuleLoad
{
    public interface IAssemblyLoader
    {
        Assembly Load(AssemblyName assemblyName, Func<IAssemblyLoadContext> contextFactory);
    }
}