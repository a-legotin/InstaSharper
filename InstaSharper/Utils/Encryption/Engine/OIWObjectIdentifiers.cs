namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class OiwObjectIdentifiers
{
    public static readonly DerObjectIdentifier MD4WithRsa = new("1.3.14.3.2.2");
    public static readonly DerObjectIdentifier MD5WithRsa = new("1.3.14.3.2.3");
    public static readonly DerObjectIdentifier MD4WithRsaEncryption = new("1.3.14.3.2.4");

    public static readonly DerObjectIdentifier DesEcb = new("1.3.14.3.2.6");
    public static readonly DerObjectIdentifier DesCbc = new("1.3.14.3.2.7");
    public static readonly DerObjectIdentifier DesOfb = new("1.3.14.3.2.8");
    public static readonly DerObjectIdentifier DesCfb = new("1.3.14.3.2.9");

    public static readonly DerObjectIdentifier DesEde = new("1.3.14.3.2.17");

    // id-SHA1 OBJECT IDENTIFIER ::=
    //   {iso(1) identified-organization(3) oiw(14) secsig(3) algorithms(2) 26 }    //
    public static readonly DerObjectIdentifier IdSha1 = new("1.3.14.3.2.26");

    public static readonly DerObjectIdentifier DsaWithSha1 = new("1.3.14.3.2.27");

    public static readonly DerObjectIdentifier Sha1WithRsa = new("1.3.14.3.2.29");

    // ElGamal Algorithm OBJECT IDENTIFIER ::=
    // {iso(1) identified-organization(3) oiw(14) dirservsig(7) algorithm(2) encryption(1) 1 }
    //
    public static readonly DerObjectIdentifier ElGamalAlgorithm = new("1.3.14.7.2.1.1");
}