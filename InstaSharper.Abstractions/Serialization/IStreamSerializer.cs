using System.IO;

namespace InstaSharper.Abstractions.Serialization
{
    public interface IStreamSerializer : ISerializer<Stream, Stream>
    {
    }
}