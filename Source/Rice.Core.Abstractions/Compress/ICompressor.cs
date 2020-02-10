using System.IO;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Compress
{
    public interface ICompressor
    {
        Task<byte[]> Compress(Stream input);
        Task<byte[]> Decompress(byte[] input);
    }
}