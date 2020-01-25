using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Transport
{
    public interface ITransportableModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPathToDll"></param>
        /// <param name="assemblyName">Optional. Only needed if the assembly name does not match the filename.</param>
        /// <returns></returns>
        Task<ITransportableModule> Create(string fullPathToDll, string assemblyName = null);
    }
}