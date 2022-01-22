using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal class BerSequenceGenerator
    : BerGenerator
{
    public BerSequenceGenerator(
        Stream outStream)
        : base(outStream)
    {
        WriteBerHeader(Asn1Tags.Constructed | Asn1Tags.Sequence);
    }

    public BerSequenceGenerator(
        Stream outStream,
        int tagNo,
        bool isExplicit)
        : base(outStream, tagNo, isExplicit)
    {
        WriteBerHeader(Asn1Tags.Constructed | Asn1Tags.Sequence);
    }
}