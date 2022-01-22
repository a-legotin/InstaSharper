using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal class BerTaggedObjectParser
    : Asn1TaggedObjectParser
{
    private readonly Asn1StreamParser _parser;

    [Obsolete]
    internal BerTaggedObjectParser(
        int baseTag,
        int tagNumber,
        Stream contentStream)
        : this((baseTag & Asn1Tags.Constructed) != 0, tagNumber, new Asn1StreamParser(contentStream))
    {
    }

    internal BerTaggedObjectParser(
        bool constructed,
        int tagNumber,
        Asn1StreamParser parser)
    {
        IsConstructed = constructed;
        TagNo = tagNumber;
        _parser = parser;
    }

    public bool IsConstructed { get; }

    public int TagNo { get; }

    public IAsn1Convertible GetObjectParser(
        int tag,
        bool isExplicit)
    {
        if (isExplicit)
        {
            if (!IsConstructed)
                throw new IOException("Explicit tags must be constructed (see X.690 8.14.2)");

            return _parser.ReadObject();
        }

        return _parser.ReadImplicit(IsConstructed, tag);
    }

    public Asn1Object ToAsn1Object()
    {
        try
        {
            return _parser.ReadTaggedObject(IsConstructed, TagNo);
        }
        catch (IOException e)
        {
            throw new Exception(e.Message);
        }
    }
}