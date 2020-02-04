using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Compress;

namespace Rice.Core.Compress.Gzip
{
    public class GzipCompressor : ICompressor
    {
        public async Task<Stream> Compress(Stream input)
        {
            var result = new MemoryStream();
            await input.CopyToAsync(new GZipStream(result, CompressionLevel.Optimal));
            return result;
        }

        public Task<Stream> Decompress(Stream input)
        {
            return Task.FromResult((Stream)new GZipStream(input, CompressionMode.Decompress));
        }
    }
}
