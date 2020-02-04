using System.IO;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.File
{
    public interface IStreamFactory
    {
        Task<Stream> OpenRead(string path);
        Task<Stream> CreateOrOverwrite(string path);
    }
}