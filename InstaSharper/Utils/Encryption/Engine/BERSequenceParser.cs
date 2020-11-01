namespace InstaSharper.Utils.Encryption.Engine
{
    internal class BerSequenceParser
        : Asn1SequenceParser
    {
        private readonly Asn1StreamParser _parser;

        internal BerSequenceParser(
            Asn1StreamParser parser) =>
            _parser = parser;

        public IAsn1Convertible ReadObject() => _parser.ReadObject();

        public Asn1Object ToAsn1Object() => new BerSequence(_parser.ReadVector());
    }
}