using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Compress;

namespace Rice.Core.Compress.Gzip
{
    public class GzipCompressor : ICompressor
    {
        public async Task<byte[]> Compress(Stream input)
        {
            if (input.CanSeek)
            {
                input.Position = 0;
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    await input.CopyToAsync(gZipStream);
                }

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> Decompress(byte[] input)
        {
            using (var compressedStream = new MemoryStream(input))
            using(var uncompressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    await gzipStream.CopyToAsync(uncompressedStream);
                }

                uncompressedStream.Position = 0;
                return uncompressedStream.ToArray();
            }
        }
    }
}
