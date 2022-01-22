using System;

namespace InstaSharper.Utils.Encryption.Engine;

/**
	 * A BER Null object.
	 */
internal class BerNull
    : DerNull
{
    public new static readonly BerNull Instance = new(0);

    [Obsolete("Use static Instance object")]
    public BerNull()
    {
    }

    private BerNull(int dummy) : base(dummy)
    {
    }

    internal override void Encode(
        DerOutputStream derOut)
    {
        if (derOut is Asn1OutputStream || derOut is BerOutputStream)
            derOut.WriteByte(Asn1Tags.Null);
        else
            base.Encode(derOut);
    }
}