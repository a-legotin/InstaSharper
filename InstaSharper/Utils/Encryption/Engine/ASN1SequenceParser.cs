namespace InstaSharper.Utils.Encryption.Engine
{
    internal interface Asn1SequenceParser
        : IAsn1Convertible
    {
        IAsn1Convertible ReadObject();
    }
}