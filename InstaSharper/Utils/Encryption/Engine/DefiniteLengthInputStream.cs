using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal class DefiniteLengthInputStream
    : LimitedInputStream
{
    private static readonly byte[] EmptyBytes = new byte[0];

    private readonly int _originalLength;

    internal DefiniteLengthInputStream(Stream inStream,
                                       int length,
                                       int limit)
        : base(inStream, limit)
    {
        if (length < 0)
            throw new ArgumentException("negative lengths not allowed", "length");

        _originalLength = length;
        Remaining = length;

        if (length == 0) SetParentEofDetect(true);
    }

    internal int Remaining { get; private set; }

    public override int ReadByte()
    {
        if (Remaining == 0)
            return -1;

        var b = _in.ReadByte();

        if (b < 0)
            throw new EndOfStreamException("DEF length " + _originalLength + " object truncated by " + Remaining);

        if (--Remaining == 0) SetParentEofDetect(true);

        return b;
    }

    public override int Read(
        byte[] buf,
        int off,
        int len)
    {
        if (Remaining == 0)
            return 0;

        var toRead = Math.Min(len, Remaining);
        var numRead = _in.Read(buf, off, toRead);

        if (numRead < 1)
            throw new EndOfStreamException("DEF length " + _originalLength + " object truncated by " + Remaining);

        if ((Remaining -= numRead) == 0) SetParentEofDetect(true);

        return numRead;
    }

    internal void ReadAllIntoByteArray(byte[] buf)
    {
        if (Remaining != buf.Length)
            throw new ArgumentException("buffer length not right for data");

        if (Remaining == 0)
            return;

        // make sure it's safe to do this!
        var limit = Limit;
        if (Remaining >= limit)
            throw new IOException("corrupted stream - out of bounds length found: " + Remaining + " >= " + limit);

        if ((Remaining -= Streams.ReadFully(_in, buf)) != 0)
            throw new EndOfStreamException("DEF length " + _originalLength + " object truncated by " + Remaining);
        SetParentEofDetect(true);
    }

    internal byte[] ToArray()
    {
        if (Remaining == 0)
            return EmptyBytes;

        // make sure it's safe to do this!
        var limit = Limit;
        if (Remaining >= limit)
            throw new IOException("corrupted stream - out of bounds length found: " + Remaining + " >= " + limit);

        var bytes = new byte[Remaining];
        if ((Remaining -= Streams.ReadFully(_in, bytes)) != 0)
            throw new EndOfStreamException("DEF length " + _originalLength + " object truncated by " + Remaining);
        SetParentEofDetect(true);
        return bytes;
    }
}