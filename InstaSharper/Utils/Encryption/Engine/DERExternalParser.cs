namespace InstaSharper.Utils.Encryption.Engine
{
    internal class DerExternalParser
        : Asn1Encodable
    {
        private readonly Asn1StreamParser _parser;

        public DerExternalParser(Asn1StreamParser parser) => _parser = parser;

        public IAsn1Convertible ReadObject() => _parser.ReadObject();

        public override Asn1Object ToAsn1Object() => new DerExternal(_parser.ReadVector());
    }
}