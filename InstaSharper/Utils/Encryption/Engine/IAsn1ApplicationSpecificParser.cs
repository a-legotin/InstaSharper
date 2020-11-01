namespace InstaSharper.Utils.Encryption.Engine
{
    internal interface IAsn1ApplicationSpecificParser
        : IAsn1Convertible
    {
        IAsn1Convertible ReadObject();
    }
}