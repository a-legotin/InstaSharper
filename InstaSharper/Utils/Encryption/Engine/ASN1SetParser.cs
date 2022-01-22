namespace InstaSharper.Utils.Encryption.Engine;

internal interface Asn1SetParser
    : IAsn1Convertible
{
    IAsn1Convertible ReadObject();
}