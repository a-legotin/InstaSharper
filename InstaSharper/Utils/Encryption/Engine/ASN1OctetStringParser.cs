using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal interface Asn1OctetStringParser
        : IAsn1Convertible
    {
        Stream GetOctetStream();
    }
}