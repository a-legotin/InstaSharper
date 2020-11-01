namespace InstaSharper.Utils.Encryption.Engine
{
    internal class BerSetParser
        : Asn1SetParser
    {
        private readonly Asn1StreamParser _parser;

        internal BerSetParser(
            Asn1StreamParser parser) =>
            _parser = parser;

        public IAsn1Convertible ReadObject() => _parser.ReadObject();

        public Asn1Object ToAsn1Object() => new BerSet(_parser.ReadVector(), false);
    }
}