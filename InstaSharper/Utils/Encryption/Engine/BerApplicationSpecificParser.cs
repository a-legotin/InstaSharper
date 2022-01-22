namespace InstaSharper.Utils.Encryption.Engine;

internal class BerApplicationSpecificParser
    : IAsn1ApplicationSpecificParser
{
    private readonly Asn1StreamParser parser;
    private readonly int tag;

    internal BerApplicationSpecificParser(
        int tag,
        Asn1StreamParser parser)
    {
        this.tag = tag;
        this.parser = parser;
    }

    public IAsn1Convertible ReadObject()
    {
        return parser.ReadObject();
    }

    public Asn1Object ToAsn1Object()
    {
        return new BerApplicationSpecific(tag, parser.ReadVector());
    }
}