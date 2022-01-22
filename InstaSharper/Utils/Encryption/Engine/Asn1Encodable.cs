using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class Asn1Encodable
    : IAsn1Convertible
{
    public const string Der = "DER";
    public const string Ber = "BER";

    public abstract Asn1Object ToAsn1Object();

    public byte[] GetEncoded()
    {
        var bOut = new MemoryStream();
        var aOut = new Asn1OutputStream(bOut);

        aOut.WriteObject(this);

        return bOut.ToArray();
    }

    public byte[] GetEncoded(
        string encoding)
    {
        if (encoding.Equals(Der))
        {
            var bOut = new MemoryStream();
            var dOut = new DerOutputStream(bOut);

            dOut.WriteObject(this);

            return bOut.ToArray();
        }

        return GetEncoded();
    }

    /**
		* Return the DER encoding of the object, null if the DER encoding can not be made.
		*
		* @return a DER byte array, null otherwise.
		*/
    public byte[] GetDerEncoded()
    {
        try
        {
            return GetEncoded(Der);
        }
        catch (IOException)
        {
            return null;
        }
    }

    public sealed override int GetHashCode()
    {
        return ToAsn1Object().CallAsn1GetHashCode();
    }

    public sealed override bool Equals(
        object obj)
    {
        if (obj == this)
            return true;

        var other = obj as IAsn1Convertible;

        if (other == null)
            return false;

        var o1 = ToAsn1Object();
        var o2 = other.ToAsn1Object();

        return o1 == o2 || null != o2 && o1.CallAsn1Equals(o2);
    }
}