using System;
using System.Diagnostics;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine;

internal class DerBitString
    : DerStringBase
{
    private static readonly char[] table
        = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

    protected readonly byte[] mData;
    protected readonly int mPadBits;

    /**
		 * @param data the octets making up the bit string.
		 * @param padBits the number of extra bits at the end of the string.
		 */
    public DerBitString(
        byte[] data,
        int padBits)
    {
        if (data == null)
            throw new ArgumentNullException("data");
        if (padBits < 0 || padBits > 7)
            throw new ArgumentException("must be in the range 0 to 7", "padBits");
        if (data.Length == 0 && padBits != 0)
            throw new ArgumentException("if 'data' is empty, 'padBits' must be 0");

        mData = Arrays.Clone(data);
        mPadBits = padBits;
    }

    public DerBitString(
        byte[] data)
        : this(data, 0)
    {
    }

    public DerBitString(
        int namedBits)
    {
        if (namedBits == 0)
        {
            mData = new byte[0];
            mPadBits = 0;
            return;
        }

        var bits = BigInteger.BitLen(namedBits);
        var bytes = (bits + 7) / 8;

        Debug.Assert(0 < bytes && bytes <= 4);

        var data = new byte[bytes];
        --bytes;

        for (var i = 0; i < bytes; i++)
        {
            data[i] = (byte)namedBits;
            namedBits >>= 8;
        }

        Debug.Assert((namedBits & 0xFF) != 0);

        data[bytes] = (byte)namedBits;

        var padBits = 0;
        while ((namedBits & (1 << padBits)) == 0) ++padBits;

        Debug.Assert(padBits < 8);

        mData = data;
        mPadBits = padBits;
    }

    public DerBitString(
        Asn1Encodable obj)
        : this(obj.GetDerEncoded())
    {
    }

    public virtual int PadBits => mPadBits;

    /**
		 * @return the value of the bit string as an int (truncating if necessary)
		 */
    public virtual int IntValue
    {
        get
        {
            int value = 0, length = Math.Min(4, mData.Length);
            for (var i = 0; i < length; ++i) value |= mData[i] << (8 * i);

            if (mPadBits > 0 && length == mData.Length)
            {
                var mask = (1 << mPadBits) - 1;
                value &= ~(mask << (8 * (length - 1)));
            }

            return value;
        }
    }

    /**
		 * return a Bit string from the passed in object
		 *
		 * @exception ArgumentException if the object cannot be converted.
		 */
    public static DerBitString GetInstance(
        object obj)
    {
        if (obj == null || obj is DerBitString) return (DerBitString)obj;

        if (obj is byte[])
            try
            {
                return (DerBitString)FromByteArray((byte[])obj);
            }
            catch (Exception e)
            {
                throw new ArgumentException("encoding error in GetInstance: " + e);
            }

        throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
    }

    /**
     * return a Bit string from a tagged object.
     * 
     * @param obj the tagged object holding the object we want
     * @param explicitly true if the object is meant to be explicitly
     * tagged false otherwise.
     * @exception ArgumentException if the tagged object cannot
     * be converted.
     */
    public static DerBitString GetInstance(
        Asn1TaggedObject obj,
        bool isExplicit)
    {
        var o = obj.GetObject();

        if (isExplicit || o is DerBitString) return GetInstance(o);

        return FromAsn1Octets(((Asn1OctetString)o).GetOctets());
    }

    /**
         * Return the octets contained in this BIT STRING, checking that this BIT STRING really
         * does represent an octet aligned string. Only use this method when the standard you are
         * following dictates that the BIT STRING will be octet aligned.
         *
         * @return a copy of the octet aligned data.
         */
    public virtual byte[] GetOctets()
    {
        if (mPadBits != 0)
            throw new InvalidOperationException("attempt to get non-octet aligned data from BIT STRING");

        return Arrays.Clone(mData);
    }

    public virtual byte[] GetBytes()
    {
        var data = Arrays.Clone(mData);

        // DER requires pad bits be zero
        if (mPadBits > 0) data[data.Length - 1] &= (byte)(0xFF << mPadBits);

        return data;
    }

    internal override void Encode(
        DerOutputStream derOut)
    {
        if (mPadBits > 0)
        {
            int last = mData[mData.Length - 1];
            var mask = (1 << mPadBits) - 1;
            var unusedBits = last & mask;

            if (unusedBits != 0)
            {
                var contents = Arrays.Prepend(mData, (byte)mPadBits);

                /*
                 * X.690-0207 11.2.1: Each unused bit in the final octet of the encoding of a bit string value shall be set to zero.
                 */
                contents[contents.Length - 1] = (byte)(last ^ unusedBits);

                derOut.WriteEncoded(Asn1Tags.BitString, contents);
                return;
            }
        }

        derOut.WriteEncoded(Asn1Tags.BitString, (byte)mPadBits, mData);
    }

    protected override int Asn1GetHashCode()
    {
        return mPadBits.GetHashCode() ^ Arrays.GetHashCode(mData);
    }

    protected override bool Asn1Equals(
        Asn1Object asn1Object)
    {
        var other = asn1Object as DerBitString;

        if (other == null)
            return false;

        return mPadBits == other.mPadBits
               && Arrays.AreEqual(mData, other.mData);
    }

    public override string GetString()
    {
        var buffer = new StringBuilder("#");

        var str = GetDerEncoded();

        for (var i = 0; i != str.Length; i++)
        {
            uint ubyte = str[i];
            buffer.Append(table[(ubyte >> 4) & 0xf]);
            buffer.Append(table[str[i] & 0xf]);
        }

        return buffer.ToString();
    }

    internal static DerBitString FromAsn1Octets(byte[] octets)
    {
        if (octets.Length < 1)
            throw new ArgumentException("truncated BIT STRING detected", "octets");

        int padBits = octets[0];
        var data = Arrays.CopyOfRange(octets, 1, octets.Length);

        if (padBits > 0 && padBits < 8 && data.Length > 0)
        {
            int last = data[data.Length - 1];
            var mask = (1 << padBits) - 1;

            if ((last & mask) != 0) return new BerBitString(data, padBits);
        }

        return new DerBitString(data, padBits);
    }
}