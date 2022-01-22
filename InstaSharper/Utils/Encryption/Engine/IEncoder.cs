using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

/**
	 * Encode and decode byte arrays (typically from binary to 7-bit ASCII
	 * encodings).
	 */
internal interface IEncoder
{
    int Encode(byte[] data,
               int off,
               int length,
               Stream outStream);

    int Decode(byte[] data,
               int off,
               int length,
               Stream outStream);

    int DecodeString(string data,
                     Stream outStream);
}