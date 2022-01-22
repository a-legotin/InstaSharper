namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class Integers
{
    private static readonly byte[] DeBruijnTZ =
    {
        0x00, 0x01, 0x02, 0x18, 0x03, 0x13, 0x06, 0x19, 0x16, 0x04, 0x14, 0x0A,
        0x10, 0x07, 0x0C, 0x1A, 0x1F, 0x17, 0x12, 0x05, 0x15, 0x09, 0x0F, 0x0B,
        0x1E, 0x11, 0x08, 0x0E, 0x1D, 0x0D, 0x1C, 0x1B
    };

    public static int NumberOfLeadingZeros(int i)
    {
        if (i <= 0)
            return (~i >> (31 - 5)) & (1 << 5);

        var u = (uint)i;
        var n = 1;
        if (0 == u >> 16)
        {
            n += 16;
            u <<= 16;
        }

        if (0 == u >> 24)
        {
            n += 8;
            u <<= 8;
        }

        if (0 == u >> 28)
        {
            n += 4;
            u <<= 4;
        }

        if (0 == u >> 30)
        {
            n += 2;
            u <<= 2;
        }

        n -= (int)(u >> 31);
        return n;
    }

    public static int NumberOfTrailingZeros(int i)
    {
        return DeBruijnTZ[(uint)((i & -i) * 0x04D7651F) >> 27];
    }

    public static int RotateLeft(int i,
                                 int distance)
    {
        return (i << distance) ^ (int)((uint)i >> -distance);
    }

    public static uint RotateLeft(uint i,
                                  int distance)
    {
        return (i << distance) ^ (i >> -distance);
    }

    public static int RotateRight(int i,
                                  int distance)
    {
        return (int)((uint)i >> distance) ^ (i << -distance);
    }

    public static uint RotateRight(uint i,
                                   int distance)
    {
        return (i >> distance) ^ (i << -distance);
    }
}