using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

// TODO Make Obsolete in favour of Asn1OutputStream?
internal class BerOutputStream
    : DerOutputStream
{
    public BerOutputStream(Stream os) : base(os)
    {
    }

    [Obsolete("Use version taking an Asn1Encodable arg instead")]
    public override void WriteObject(
        object obj)
    {
        if (obj == null)
            WriteNull();
        else if (obj is Asn1Object)
            ((Asn1Object)obj).Encode(this);
        else if (obj is Asn1Encodable)
            ((Asn1Encodable)obj).ToAsn1Object().Encode(this);
        else
            throw new IOException("object not BerEncodable");
    }
}