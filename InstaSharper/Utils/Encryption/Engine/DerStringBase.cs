namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class DerStringBase
        : Asn1Object, IAsn1String
    {
        public abstract string GetString();

        public override string ToString() => GetString();

        protected override int Asn1GetHashCode() => GetString().GetHashCode();
    }
}