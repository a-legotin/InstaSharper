namespace InstaSharper.Utils.Encryption.Engine;

public sealed class TeleTrusTObjectIdentifiers
{
    internal static readonly DerObjectIdentifier TeleTrusTAlgorithm = new("1.3.36.3");

    internal static readonly DerObjectIdentifier RipeMD160 = new(TeleTrusTAlgorithm + ".2.1");
    internal static readonly DerObjectIdentifier RipeMD128 = new(TeleTrusTAlgorithm + ".2.2");
    internal static readonly DerObjectIdentifier RipeMD256 = new(TeleTrusTAlgorithm + ".2.3");

    internal static readonly DerObjectIdentifier TeleTrusTRsaSignatureAlgorithm = new(TeleTrusTAlgorithm + ".3.1");

    internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD160 =
        new(TeleTrusTRsaSignatureAlgorithm + ".2");

    internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD128 =
        new(TeleTrusTRsaSignatureAlgorithm + ".3");

    internal static readonly DerObjectIdentifier RsaSignatureWithRipeMD256 =
        new(TeleTrusTRsaSignatureAlgorithm + ".4");

    internal static readonly DerObjectIdentifier ECSign = new(TeleTrusTAlgorithm + ".3.2");

    internal static readonly DerObjectIdentifier ECSignWithSha1 = new(ECSign + ".1");
    internal static readonly DerObjectIdentifier ECSignWithRipeMD160 = new(ECSign + ".2");

    internal static readonly DerObjectIdentifier
        EccBrainpool = new(TeleTrusTAlgorithm + ".3.2.8");

    internal static readonly DerObjectIdentifier EllipticCurve = new(EccBrainpool + ".1");
    internal static readonly DerObjectIdentifier VersionOne = new(EllipticCurve + ".1");

    internal static readonly DerObjectIdentifier BrainpoolP160R1 = new(VersionOne + ".1");
    internal static readonly DerObjectIdentifier BrainpoolP160T1 = new(VersionOne + ".2");
    internal static readonly DerObjectIdentifier BrainpoolP192R1 = new(VersionOne + ".3");
    internal static readonly DerObjectIdentifier BrainpoolP192T1 = new(VersionOne + ".4");
    internal static readonly DerObjectIdentifier BrainpoolP224R1 = new(VersionOne + ".5");
    internal static readonly DerObjectIdentifier BrainpoolP224T1 = new(VersionOne + ".6");
    internal static readonly DerObjectIdentifier BrainpoolP256R1 = new(VersionOne + ".7");
    internal static readonly DerObjectIdentifier BrainpoolP256T1 = new(VersionOne + ".8");
    internal static readonly DerObjectIdentifier BrainpoolP320R1 = new(VersionOne + ".9");
    internal static readonly DerObjectIdentifier BrainpoolP320T1 = new(VersionOne + ".10");
    internal static readonly DerObjectIdentifier BrainpoolP384R1 = new(VersionOne + ".11");
    internal static readonly DerObjectIdentifier BrainpoolP384T1 = new(VersionOne + ".12");
    internal static readonly DerObjectIdentifier BrainpoolP512R1 = new(VersionOne + ".13");
    internal static readonly DerObjectIdentifier BrainpoolP512T1 = new(VersionOne + ".14");

    private TeleTrusTObjectIdentifiers()
    {
    }
}