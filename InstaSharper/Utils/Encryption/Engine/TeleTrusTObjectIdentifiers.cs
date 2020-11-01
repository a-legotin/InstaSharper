namespace InstaSharper.Utils.Encryption.Engine
{
    public sealed class TeleTrusTObjectIdentifiers
    {
        internal static readonly DerObjectIdentifier TeleTrusTAlgorithm = new DerObjectIdentifier("1.3.36.3");

        internal static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier(TeleTrusTAlgorithm + ".2.1");
        internal static readonly DerObjectIdentifier RipeMD128 = new DerObjectIdentifier(TeleTrusTAlgorithm + ".2.2");
        internal static readonly DerObjectIdentifier RipeMD256 = new DerObjectIdentifier(TeleTrusTAlgorithm + ".2.3");

        internal static readonly DerObjectIdentifier TeleTrusTRsaSignatureAlgorithm =
            new DerObjectIdentifier(TeleTrusTAlgorithm + ".3.1");

        internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD160 =
            new DerObjectIdentifier(TeleTrusTRsaSignatureAlgorithm + ".2");

        internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD128 =
            new DerObjectIdentifier(TeleTrusTRsaSignatureAlgorithm + ".3");

        internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD256 =
            new DerObjectIdentifier(TeleTrusTRsaSignatureAlgorithm + ".4");

        internal static readonly DerObjectIdentifier ECSign = new DerObjectIdentifier(TeleTrusTAlgorithm + ".3.2");

        internal static readonly DerObjectIdentifier ECSignWithSha1 = new DerObjectIdentifier(ECSign + ".1");
        internal static readonly DerObjectIdentifier ECSignWithRipeMD160 = new DerObjectIdentifier(ECSign + ".2");

        internal static readonly DerObjectIdentifier
            EccBrainpool = new DerObjectIdentifier(TeleTrusTAlgorithm + ".3.2.8");

        internal static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier(EccBrainpool + ".1");
        internal static readonly DerObjectIdentifier VersionOne = new DerObjectIdentifier(EllipticCurve + ".1");

        internal static readonly DerObjectIdentifier BrainpoolP160R1 = new DerObjectIdentifier(VersionOne + ".1");
        internal static readonly DerObjectIdentifier BrainpoolP160T1 = new DerObjectIdentifier(VersionOne + ".2");
        internal static readonly DerObjectIdentifier BrainpoolP192R1 = new DerObjectIdentifier(VersionOne + ".3");
        internal static readonly DerObjectIdentifier BrainpoolP192T1 = new DerObjectIdentifier(VersionOne + ".4");
        internal static readonly DerObjectIdentifier BrainpoolP224R1 = new DerObjectIdentifier(VersionOne + ".5");
        internal static readonly DerObjectIdentifier BrainpoolP224T1 = new DerObjectIdentifier(VersionOne + ".6");
        internal static readonly DerObjectIdentifier BrainpoolP256R1 = new DerObjectIdentifier(VersionOne + ".7");
        internal static readonly DerObjectIdentifier BrainpoolP256T1 = new DerObjectIdentifier(VersionOne + ".8");
        internal static readonly DerObjectIdentifier BrainpoolP320R1 = new DerObjectIdentifier(VersionOne + ".9");
        internal static readonly DerObjectIdentifier BrainpoolP320T1 = new DerObjectIdentifier(VersionOne + ".10");
        internal static readonly DerObjectIdentifier BrainpoolP384R1 = new DerObjectIdentifier(VersionOne + ".11");
        internal static readonly DerObjectIdentifier BrainpoolP384T1 = new DerObjectIdentifier(VersionOne + ".12");
        internal static readonly DerObjectIdentifier BrainpoolP512R1 = new DerObjectIdentifier(VersionOne + ".13");
        internal static readonly DerObjectIdentifier BrainpoolP512T1 = new DerObjectIdentifier(VersionOne + ".14");

        private TeleTrusTObjectIdentifiers()
        {
        }
    }
}