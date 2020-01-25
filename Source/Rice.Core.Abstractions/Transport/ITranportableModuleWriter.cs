using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Transport
{
    public interface ITranportableModuleWriter
    {
        /// <summary>
        /// Creates (or overwrites) a module on disk
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        Task WriteToFile(string fullPath, ITransportableModule module);
    }
}