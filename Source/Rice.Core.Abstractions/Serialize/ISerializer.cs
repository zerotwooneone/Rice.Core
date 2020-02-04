using System.IO;
using System.Threading.Tasks;

namespace Rice.Core.Abstractions.Serialize
{
    public interface ISerializer
    {
        Task<Stream> Serialize(ISerializableModule module);
        Task<ISerializableModule> Deserialize(Stream stream);
    }
}