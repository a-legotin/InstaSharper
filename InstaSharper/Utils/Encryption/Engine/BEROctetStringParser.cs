using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class BerOctetStringParser
        : Asn1OctetStringParser
    {
        private readonly Asn1StreamParser _parser;

        internal BerOctetStringParser(
            Asn1StreamParser parser) =>
            _parser = parser;

        public Stream GetOctetStream() => new ConstructedOctetStream(_parser);

        public Asn1Object ToAsn1Object()
        {
            try
            {
                return new BerOctetString(Streams.ReadAll(GetOctetStream()));
            }
            catch (IOException e)
            {
                throw new Exception("IOException converting stream to byte array: " + e.Message, e);
            }
        }
    }
}