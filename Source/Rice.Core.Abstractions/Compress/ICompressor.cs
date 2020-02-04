using System.IO;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Compress
{
    public interface ICompressor
    {
        Task<Stream> Compress(Stream input);
        Task<Stream> Decompress(Stream input);
    }
}