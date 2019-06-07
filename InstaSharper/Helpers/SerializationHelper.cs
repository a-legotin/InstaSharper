using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using InstaSharper.Classes;

namespace InstaSharper.Helpers
{
    internal static class SerializationHelper
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

        public static string SerializeToBase64(StateData o)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static T DeserializeFromBase64<T>(string base64)
        {
            byte[] b = Convert.FromBase64String(base64);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }
    }
}