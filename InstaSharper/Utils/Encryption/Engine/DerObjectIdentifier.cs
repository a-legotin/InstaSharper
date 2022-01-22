using System;
using System.IO;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine;

internal class DerObjectIdentifier
    : Asn1Object
{
    private const long LONG_LIMIT = (long.MaxValue >> 7) - 0x7f;

    private static readonly DerObjectIdentifier[] cache = new DerObjectIdentifier[1024];

    private byte[] body;

    public DerObjectIdentifier(
        string identifier)
    {
        if (identifier == null)
            throw new ArgumentNullException("identifier");
        if (!IsValidIdentifier(identifier))
            throw new FormatException("string " + identifier + " not an OID");

        Id = identifier;
    }

    internal DerObjectIdentifier(DerObjectIdentifier oid,
                                 string branchID)
    {
        if (!IsValidBranchID(branchID, 0))
            throw new ArgumentException("string " + branchID + " not a valid OID branch", "branchID");

        Id = oid.Id + "." + branchID;
    }

    internal DerObjectIdentifier(byte[] bytes)
    {
        Id = MakeOidStringFromBytes(bytes);
        body = Arrays.Clone(bytes);
    }

    // TODO Change to ID?
    public string Id { get; }

    /**
         * return an Oid from the passed in object
         *
         * @exception ArgumentException if the object cannot be converted.
         */
    public static DerObjectIdentifier GetInstance(object obj)
    {
        if (obj == null || obj is DerObjectIdentifier)
            return (DerObjectIdentifier)obj;

        if (obj is Asn1Encodable)
        {
            var asn1Obj = ((Asn1Encodable)obj).ToAsn1Object();

            if (asn1Obj is DerObjectIdentifier)
                return (DerObjectIdentifier)asn1Obj;
        }

        if (obj is byte[])
            return FromOctetString((byte[])obj);

        throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
    }

    /**
     * return an object Identifier from a tagged object.
     * 
     * @param obj the tagged object holding the object we want
     * @param explicitly true if the object is meant to be explicitly
     * tagged false otherwise.
     * @exception ArgumentException if the tagged object cannot
     * be converted.
     */
    public static DerObjectIdentifier GetInstance(
        Asn1TaggedObject obj,
        bool explicitly)
    {
        var o = obj.GetObject();

        if (explicitly || o is DerObjectIdentifier) return GetInstance(o);

        return FromOctetString(Asn1OctetString.GetInstance(o).GetOctets());
    }

    public virtual DerObjectIdentifier Branch(string branchID)
    {
        return new DerObjectIdentifier(this, branchID);
    }

    /**
         * Return  true if this oid is an extension of the passed in branch, stem.
         * @param stem the arc or branch that is a possible parent.
         * @return  true if the branch is on the passed in stem, false otherwise.
         */
    public virtual bool On(DerObjectIdentifier stem)
    {
        string id = Id, stemId = stem.Id;
        return id.Length > stemId.Length && id[stemId.Length] == '.' && Platform.StartsWith(id, stemId);
    }

    private void WriteField(
        Stream outputStream,
        long fieldValue)
    {
        var result = new byte[9];
        var pos = 8;
        result[pos] = (byte)(fieldValue & 0x7f);
        while (fieldValue >= 1L << 7)
        {
            fieldValue >>= 7;
            result[--pos] = (byte)((fieldValue & 0x7f) | 0x80);
        }

        outputStream.Write(result, pos, 9 - pos);
    }

    private void WriteField(
        Stream outputStream,
        BigInteger fieldValue)
    {
        var byteCount = (fieldValue.BitLength + 6) / 7;
        if (byteCount == 0)
        {
            outputStream.WriteByte(0);
        }
        else
        {
            var tmpValue = fieldValue;
            var tmp = new byte[byteCount];
            for (var i = byteCount - 1; i >= 0; i--)
            {
                tmp[i] = (byte)((tmpValue.IntValue & 0x7f) | 0x80);
                tmpValue = tmpValue.ShiftRight(7);
            }

            tmp[byteCount - 1] &= 0x7f;
            outputStream.Write(tmp, 0, tmp.Length);
        }
    }

    private void DoOutput(MemoryStream bOut)
    {
        var tok = new OidTokenizer(Id);

        var token = tok.NextToken();
        var first = int.Parse(token) * 40;

        token = tok.NextToken();
        if (token.Length <= 18)
            WriteField(bOut, first + long.Parse(token));
        else
            WriteField(bOut, new BigInteger(token).Add(BigInteger.ValueOf(first)));

        while (tok.HasMoreTokens)
        {
            token = tok.NextToken();
            if (token.Length <= 18)
                WriteField(bOut, long.Parse(token));
            else
                WriteField(bOut, new BigInteger(token));
        }
    }

    internal byte[] GetBody()
    {
        lock (this)
        {
            if (body == null)
            {
                var bOut = new MemoryStream();
                DoOutput(bOut);
                body = bOut.ToArray();
            }
        }

        return body;
    }

    internal override void Encode(
        DerOutputStream derOut)
    {
        derOut.WriteEncoded(Asn1Tags.ObjectIdentifier, GetBody());
    }

    protected override int Asn1GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected override bool Asn1Equals(
        Asn1Object asn1Object)
    {
        var other = asn1Object as DerObjectIdentifier;

        if (other == null)
            return false;

        return Id.Equals(other.Id);
    }

    public override string ToString()
    {
        return Id;
    }

    private static bool IsValidBranchID(string branchID,
                                        int start)
    {
        var digitCount = 0;

        var pos = branchID.Length;
        while (--pos >= start)
        {
            var ch = branchID[pos];

            if (ch == '.')
            {
                if (0 == digitCount || digitCount > 1 && branchID[pos + 1] == '0')
                    return false;

                digitCount = 0;
            }
            else if ('0' <= ch && ch <= '9')
            {
                ++digitCount;
            }
            else
            {
                return false;
            }
        }

        if (0 == digitCount || digitCount > 1 && branchID[pos + 1] == '0')
            return false;

        return true;
    }

    private static bool IsValidIdentifier(string identifier)
    {
        if (identifier.Length < 3 || identifier[1] != '.')
            return false;

        var first = identifier[0];
        if (first < '0' || first > '2')
            return false;

        return IsValidBranchID(identifier, 2);
    }

    private static string MakeOidStringFromBytes(
        byte[] bytes)
    {
        var objId = new StringBuilder();
        long value = 0;
        BigInteger bigValue = null;
        var first = true;

        for (var i = 0; i != bytes.Length; i++)
        {
            int b = bytes[i];

            if (value <= LONG_LIMIT)
            {
                value += b & 0x7f;
                if ((b & 0x80) == 0) // end of number reached
                {
                    if (first)
                    {
                        if (value < 40)
                        {
                            objId.Append('0');
                        }
                        else if (value < 80)
                        {
                            objId.Append('1');
                            value -= 40;
                        }
                        else
                        {
                            objId.Append('2');
                            value -= 80;
                        }

                        first = false;
                    }

                    objId.Append('.');
                    objId.Append(value);
                    value = 0;
                }
                else
                {
                    value <<= 7;
                }
            }
            else
            {
                if (bigValue == null) bigValue = BigInteger.ValueOf(value);

                bigValue = bigValue.Or(BigInteger.ValueOf(b & 0x7f));
                if ((b & 0x80) == 0)
                {
                    if (first)
                    {
                        objId.Append('2');
                        bigValue = bigValue.Subtract(BigInteger.ValueOf(80));
                        first = false;
                    }

                    objId.Append('.');
                    objId.Append(bigValue);
                    bigValue = null;
                    value = 0;
                }
                else
                {
                    bigValue = bigValue.ShiftLeft(7);
                }
            }
        }

        return objId.ToString();
    }

    internal static DerObjectIdentifier FromOctetString(byte[] enc)
    {
        var hashCode = Arrays.GetHashCode(enc);
        var first = hashCode & 1023;

        lock (cache)
        {
            var entry = cache[first];
            if (entry != null && Arrays.AreEqual(enc, entry.GetBody())) return entry;

            return cache[first] = new DerObjectIdentifier(enc);
        }
    }
}