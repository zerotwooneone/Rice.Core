using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Transport
{
    public delegate IEnumerable<(string fullPath, string assemblyName)> FindDependencyStrategy(string fullPathToDll,
        string assemblyName = null);
    public interface ITransportableModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="fullPathToDll"></param>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        Task<ITransportableModule> Create(string assemblyName,
            string fullPathToDll,
            IEnumerable<(string fullPath, string assemblyName)> dependencies = null);

        Task<ITransportableModule> Create(string assemblyName,
            string fullPathToDll,
            FindDependencyStrategy findDependencyStrategy)
        {
            var dependencies = findDependencyStrategy(fullPathToDll, assemblyName);
            return Create(assemblyName, fullPathToDll, dependencies);
        }
    }
}