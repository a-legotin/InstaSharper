namespace InstaSharper.Utils.Encryption.Engine;

internal class BerApplicationSpecific
    : DerApplicationSpecific
{
    public BerApplicationSpecific(
        int tagNo,
        Asn1EncodableVector vec)
        : base(tagNo, vec)
    {
    }
}