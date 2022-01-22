using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal class HexEncoder
    : IEncoder
{
    /*
     * set up the decoding table.
     */
    protected readonly byte[] decodingTable = new byte[128];

    protected readonly byte[] encodingTable =
    {
        (byte)'0', (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5', (byte)'6', (byte)'7',
        (byte)'8', (byte)'9', (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f'
    };

    public HexEncoder()
    {
        InitialiseDecodingTable();
    }

    /**
        * encode the input data producing a Hex output stream.
        *
        * @return the number of bytes produced.
        */
    public int Encode(byte[] buf,
                      int off,
                      int len,
                      Stream outStream)
    {
        var tmp = new byte[72];
        while (len > 0)
        {
            var inLen = Math.Min(36, len);
            var outLen = Encode(buf, off, inLen, tmp, 0);
            outStream.Write(tmp, 0, outLen);
            off += inLen;
            len -= inLen;
        }

        return len * 2;
    }

    /**
        * decode the Hex encoded byte data writing it to the given output stream,
        * whitespace characters will be ignored.
        *
        * @return the number of bytes produced.
        */
    public int Decode(
        byte[] data,
        int off,
        int length,
        Stream outStream)
    {
        byte b1, b2;
        var outLen = 0;
        var buf = new byte[36];
        var bufOff = 0;
        var end = off + length;

        while (end > off)
        {
            if (!Ignore((char)data[end - 1])) break;

            end--;
        }

        var i = off;
        while (i < end)
        {
            while (i < end && Ignore((char)data[i])) i++;

            b1 = decodingTable[data[i++]];

            while (i < end && Ignore((char)data[i])) i++;

            b2 = decodingTable[data[i++]];

            if ((b1 | b2) >= 0x80)
                throw new IOException("invalid characters encountered in Hex data");

            buf[bufOff++] = (byte)((b1 << 4) | b2);

            if (bufOff == buf.Length)
            {
                outStream.Write(buf, 0, bufOff);
                bufOff = 0;
            }

            outLen++;
        }

        if (bufOff > 0) outStream.Write(buf, 0, bufOff);

        return outLen;
    }

    /**
        * decode the Hex encoded string data writing it to the given output stream,
        * whitespace characters will be ignored.
        *
        * @return the number of bytes produced.
        */
    public int DecodeString(
        string data,
        Stream outStream)
    {
        byte b1, b2;
        var length = 0;
        var buf = new byte[36];
        var bufOff = 0;
        var end = data.Length;

        while (end > 0)
        {
            if (!Ignore(data[end - 1])) break;

            end--;
        }

        var i = 0;
        while (i < end)
        {
            while (i < end && Ignore(data[i])) i++;

            b1 = decodingTable[data[i++]];

            while (i < end && Ignore(data[i])) i++;

            b2 = decodingTable[data[i++]];

            if ((b1 | b2) >= 0x80)
                throw new IOException("invalid characters encountered in Hex data");

            buf[bufOff++] = (byte)((b1 << 4) | b2);

            if (bufOff == buf.Length)
            {
                outStream.Write(buf, 0, bufOff);
                bufOff = 0;
            }

            length++;
        }

        if (bufOff > 0) outStream.Write(buf, 0, bufOff);

        return length;
    }

    protected void InitialiseDecodingTable()
    {
        Arrays.Fill(decodingTable, 0xff);

        for (var i = 0; i < encodingTable.Length; i++) decodingTable[encodingTable[i]] = (byte)i;

        decodingTable['A'] = decodingTable['a'];
        decodingTable['B'] = decodingTable['b'];
        decodingTable['C'] = decodingTable['c'];
        decodingTable['D'] = decodingTable['d'];
        decodingTable['E'] = decodingTable['e'];
        decodingTable['F'] = decodingTable['f'];
    }

    public int Encode(byte[] inBuf,
                      int inOff,
                      int inLen,
                      byte[] outBuf,
                      int outOff)
    {
        var inPos = inOff;
        var inEnd = inOff + inLen;
        var outPos = outOff;

        while (inPos < inEnd)
        {
            uint b = inBuf[inPos++];

            outBuf[outPos++] = encodingTable[b >> 4];
            outBuf[outPos++] = encodingTable[b & 0xF];
        }

        return outPos - outOff;
    }

    private static bool Ignore(char c)
    {
        return c == '\n' || c == '\r' || c == '\t' || c == ' ';
    }

    internal byte[] DecodeStrict(string str,
                                 int off,
                                 int len)
    {
        if (null == str)
            throw new ArgumentNullException("str");
        if (off < 0 || len < 0 || off > str.Length - len)
            throw new IndexOutOfRangeException("invalid offset and/or length specified");
        if (0 != (len & 1))
            throw new ArgumentException("a hexadecimal encoding must have an even number of characters", "len");

        var resultLen = len >> 1;
        var result = new byte[resultLen];

        var strPos = off;
        for (var i = 0; i < resultLen; ++i)
        {
            var b1 = decodingTable[str[strPos++]];
            var b2 = decodingTable[str[strPos++]];

            if ((b1 | b2) >= 0x80)
                throw new IOException("invalid characters encountered in Hex data");

            result[i] = (byte)((b1 << 4) | b2);
        }

        return result;
    }
}