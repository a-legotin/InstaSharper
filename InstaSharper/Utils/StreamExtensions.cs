using System.IO;

namespace InstaSharper.Utils
{
    internal static class StreamExtensions
    {
        internal static byte[] ToByteArray(this Stream stream)
        {
            if (stream is MemoryStream memoryStream)
                return memoryStream.ToArray();
            using var ms = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}