using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Serialize
{
    public interface ISerializableFactory
    {
        Task<ISerializableModule> CreateModule(string assemblyName, 
            Stream assemblyStream,
            IEnumerable<ISerializableDependency> dependencies);
        Task<ISerializableDependency> CreateDependency(string assemblyName, Stream assemblyStream);
    }
}