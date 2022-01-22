using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using InstaSharper.Abstractions.Serialization;

namespace InstaSharper.Serialization;

internal class StreamSerializer : IStreamSerializer
{
    public T Deserialize<T>(Stream stream)
    {
        var formatter = new BinaryFormatter();
        stream.Seek(0, SeekOrigin.Begin);
        var fromStream = formatter.Deserialize(stream);
        return (T)fromStream;
    }

    public Stream Serialize<T>(T obj)
    {
        var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, obj);
        return stream;
    }
}