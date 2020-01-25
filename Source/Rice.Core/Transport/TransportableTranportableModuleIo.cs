using System;
using System.IO;
using System.Threading.Tasks;
using Rice.Core.Abstractions.Transport;

namespace Rice.Core.Transport
{
    public class TransportableTranportableModuleIo : ITransportableModuleFactory, ITranportableModuleWriter
    {
        private const long ArbitraryMaxModuleSize = 50000;

        public async Task<ITransportableModule> Create(string fullPathToDll, string assemblyName = null)
        {
            if (fullPathToDll == null) throw new ArgumentNullException(nameof(fullPathToDll));
            var dllFileInfo = new FileInfo(fullPathToDll);
            if(!dllFileInfo.Exists) throw new ArgumentException("File not found");
            if (dllFileInfo.Length > ArbitraryMaxModuleSize) throw new ArgumentException("File size limit exceeded");

            var nameToUse = string.IsNullOrWhiteSpace(assemblyName)
                ? Path.GetFileNameWithoutExtension(dllFileInfo.Name)
                : assemblyName;
            
            using (var fileStream = dllFileInfo.OpenRead())
            {
                byte[] buffer = new byte[dllFileInfo.Length];
                var count = await fileStream
                    .ReadAsync(buffer, 0, (int) dllFileInfo.Length)
                    .ConfigureAwait(false);    
                return new TransportableModule(nameToUse, buffer);
            }
        }

        public async Task WriteToFile(string fullPath, ITransportableModule module)
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await fs.WriteAsync(module.Bytes, 0, module.Bytes.Length);
            }
        }
    }
}
