using System.IO;

namespace InstaSharper.Utils.Encryption.Engine.digests;

internal class NullDigest : IDigest
{
    private readonly MemoryStream bOut = new();

    public string AlgorithmName => "NULL";

    public int GetByteLength()
    {
        // TODO Is this okay?
        return 0;
    }

    public int GetDigestSize()
    {
        return (int)bOut.Length;
    }

    public void Update(byte b)
    {
        bOut.WriteByte(b);
    }

    public void BlockUpdate(byte[] inBytes,
                            int inOff,
                            int len)
    {
        bOut.Write(inBytes, inOff, len);
    }

    public int DoFinal(byte[] outBytes,
                       int outOff)
    {
        try
        {
            return Streams.WriteBufTo(bOut, outBytes, outOff);
        }
        finally
        {
            Reset();
        }
    }

    public void Reset()
    {
        bOut.SetLength(0);
    }
}