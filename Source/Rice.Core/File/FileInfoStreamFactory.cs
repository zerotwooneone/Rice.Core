using System.IO;
using System.Threading.Tasks;
using Rice.Core.Abstractions.File;

namespace Rice.Core.File
{
    public class FileInfoStreamFactory : IStreamFactory
    {
        public Task<Stream> OpenRead(string path)
        {
            return Task.FromResult((Stream)new FileInfo(path).OpenRead());
        }

        public Task<Stream> CreateOrOverwrite(string path)
        {
            var fileInfo = new FileInfo(path);
            return Task.FromResult((Stream)new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write, FileShare.Read));
        }
    }
}
