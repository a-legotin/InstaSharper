using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace InstaSharper.Helpers
{
    internal class SerializationHelper
    {
        public static MemoryStream SerializeToStream(object o)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        public static T DeserializeFromStream<T>(Stream stream)
        {
            var formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            var fromStream = formatter.Deserialize(stream);
            return (T) fromStream;
        }
    }
}