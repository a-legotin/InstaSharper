using System;

namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class PkcsObjectIdentifiers
{
    //
    // pkcs-1 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 1 }
    //
    public const string Pkcs1 = "1.2.840.113549.1.1";

    //
    // pkcs-3 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 3 }
    //
    public const string Pkcs3 = "1.2.840.113549.1.3";

    //
    // pkcs-5 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 5 }
    //
    public const string Pkcs5 = "1.2.840.113549.1.5";

    //
    // encryptionAlgorithm OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) 3 }
    //
    public const string EncryptionAlgorithm = "1.2.840.113549.3";

    //
    // object identifiers for digests
    //
    public const string DigestAlgorithm = "1.2.840.113549.2";

    //
    // pkcs-7 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 7 }
    //
    public const string Pkcs7 = "1.2.840.113549.1.7";

    //
    // pkcs-9 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 9 }
    //
    public const string Pkcs9 = "1.2.840.113549.1.9";

    public const string CertTypes = Pkcs9 + ".22";

    public const string CrlTypes = Pkcs9 + ".23";

    //
    // id-ct OBJECT IDENTIFIER ::= {iso(1) member-body(2) usa(840)
    // rsadsi(113549) pkcs(1) pkcs-9(9) smime(16) ct(1)}
    //
    public const string IdCT = "1.2.840.113549.1.9.16.1";

    //
    // id-cti OBJECT IDENTIFIER ::= {iso(1) member-body(2) usa(840)
    // rsadsi(113549) pkcs(1) pkcs-9(9) smime(16) cti(6)}
    //
    public const string IdCti = "1.2.840.113549.1.9.16.6";

    //
    // id-aa OBJECT IDENTIFIER ::= {iso(1) member-body(2) usa(840)
    // rsadsi(113549) pkcs(1) pkcs-9(9) smime(16) attributes(2)}
    //
    public const string IdAA = "1.2.840.113549.1.9.16.2";

    //
    // id-spq OBJECT IDENTIFIER ::= {iso(1) member-body(2) usa(840)
    // rsadsi(113549) pkcs(1) pkcs-9(9) smime(16) id-spq(5)}
    //
    public const string IdSpq = "1.2.840.113549.1.9.16.5";

    //
    // pkcs-12 OBJECT IDENTIFIER ::= {
    //       iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 12 }
    //
    public const string Pkcs12 = "1.2.840.113549.1.12";
    public const string BagTypes = Pkcs12 + ".10.1";

    public const string Pkcs12PbeIds = Pkcs12 + ".1";
    internal static readonly DerObjectIdentifier Pkcs1Oid = new(Pkcs1);

    public static readonly DerObjectIdentifier RsaEncryption = Pkcs1Oid.Branch("1");
    public static readonly DerObjectIdentifier MD2WithRsaEncryption = Pkcs1Oid.Branch("2");
    public static readonly DerObjectIdentifier MD4WithRsaEncryption = Pkcs1Oid.Branch("3");
    public static readonly DerObjectIdentifier MD5WithRsaEncryption = Pkcs1Oid.Branch("4");
    public static readonly DerObjectIdentifier Sha1WithRsaEncryption = Pkcs1Oid.Branch("5");
    public static readonly DerObjectIdentifier SrsaOaepEncryptionSet = Pkcs1Oid.Branch("6");
    public static readonly DerObjectIdentifier IdRsaesOaep = Pkcs1Oid.Branch("7");
    public static readonly DerObjectIdentifier IdMgf1 = Pkcs1Oid.Branch("8");
    public static readonly DerObjectIdentifier IdPSpecified = Pkcs1Oid.Branch("9");
    public static readonly DerObjectIdentifier IdRsassaPss = Pkcs1Oid.Branch("10");
    public static readonly DerObjectIdentifier Sha256WithRsaEncryption = Pkcs1Oid.Branch("11");
    public static readonly DerObjectIdentifier Sha384WithRsaEncryption = Pkcs1Oid.Branch("12");
    public static readonly DerObjectIdentifier Sha512WithRsaEncryption = Pkcs1Oid.Branch("13");
    public static readonly DerObjectIdentifier Sha224WithRsaEncryption = Pkcs1Oid.Branch("14");

    /**
     * PKCS#1: 1.2.840.113549.1.1.15
     */
    public static readonly DerObjectIdentifier Sha512_224WithRSAEncryption = Pkcs1Oid.Branch("15");

    /**
     * PKCS#1: 1.2.840.113549.1.1.16
     */
    public static readonly DerObjectIdentifier Sha512_256WithRSAEncryption = Pkcs1Oid.Branch("16");

    public static readonly DerObjectIdentifier DhKeyAgreement = new(Pkcs3 + ".1");

    public static readonly DerObjectIdentifier PbeWithMD2AndDesCbc = new(Pkcs5 + ".1");
    public static readonly DerObjectIdentifier PbeWithMD2AndRC2Cbc = new(Pkcs5 + ".4");
    public static readonly DerObjectIdentifier PbeWithMD5AndDesCbc = new(Pkcs5 + ".3");
    public static readonly DerObjectIdentifier PbeWithMD5AndRC2Cbc = new(Pkcs5 + ".6");
    public static readonly DerObjectIdentifier PbeWithSha1AndDesCbc = new(Pkcs5 + ".10");
    public static readonly DerObjectIdentifier PbeWithSha1AndRC2Cbc = new(Pkcs5 + ".11");

    public static readonly DerObjectIdentifier IdPbeS2 = new(Pkcs5 + ".13");
    public static readonly DerObjectIdentifier IdPbkdf2 = new(Pkcs5 + ".12");

    public static readonly DerObjectIdentifier DesEde3Cbc = new(EncryptionAlgorithm + ".7");
    public static readonly DerObjectIdentifier RC2Cbc = new(EncryptionAlgorithm + ".2");
    public static readonly DerObjectIdentifier rc4 = new(EncryptionAlgorithm + ".4");

    //
    // md2 OBJECT IDENTIFIER ::=
    //      {iso(1) member-body(2) US(840) rsadsi(113549) DigestAlgorithm(2) 2}
    //
    public static readonly DerObjectIdentifier MD2 = new(DigestAlgorithm + ".2");

    //
    // md4 OBJECT IDENTIFIER ::=
    //      {iso(1) member-body(2) US(840) rsadsi(113549) DigestAlgorithm(2) 4}
    //
    public static readonly DerObjectIdentifier MD4 = new(DigestAlgorithm + ".4");

    //
    // md5 OBJECT IDENTIFIER ::=
    //      {iso(1) member-body(2) US(840) rsadsi(113549) DigestAlgorithm(2) 5}
    //
    public static readonly DerObjectIdentifier MD5 = new(DigestAlgorithm + ".5");

    public static readonly DerObjectIdentifier IdHmacWithSha1 = new(DigestAlgorithm + ".7");
    public static readonly DerObjectIdentifier IdHmacWithSha224 = new(DigestAlgorithm + ".8");
    public static readonly DerObjectIdentifier IdHmacWithSha256 = new(DigestAlgorithm + ".9");
    public static readonly DerObjectIdentifier IdHmacWithSha384 = new(DigestAlgorithm + ".10");
    public static readonly DerObjectIdentifier IdHmacWithSha512 = new(DigestAlgorithm + ".11");

    public static readonly DerObjectIdentifier Data = new(Pkcs7 + ".1");
    public static readonly DerObjectIdentifier SignedData = new(Pkcs7 + ".2");
    public static readonly DerObjectIdentifier EnvelopedData = new(Pkcs7 + ".3");
    public static readonly DerObjectIdentifier SignedAndEnvelopedData = new(Pkcs7 + ".4");
    public static readonly DerObjectIdentifier DigestedData = new(Pkcs7 + ".5");
    public static readonly DerObjectIdentifier EncryptedData = new(Pkcs7 + ".6");

    public static readonly DerObjectIdentifier Pkcs9AtEmailAddress = new(Pkcs9 + ".1");
    public static readonly DerObjectIdentifier Pkcs9AtUnstructuredName = new(Pkcs9 + ".2");
    public static readonly DerObjectIdentifier Pkcs9AtContentType = new(Pkcs9 + ".3");
    public static readonly DerObjectIdentifier Pkcs9AtMessageDigest = new(Pkcs9 + ".4");
    public static readonly DerObjectIdentifier Pkcs9AtSigningTime = new(Pkcs9 + ".5");
    public static readonly DerObjectIdentifier Pkcs9AtCounterSignature = new(Pkcs9 + ".6");
    public static readonly DerObjectIdentifier Pkcs9AtChallengePassword = new(Pkcs9 + ".7");
    public static readonly DerObjectIdentifier Pkcs9AtUnstructuredAddress = new(Pkcs9 + ".8");

    public static readonly DerObjectIdentifier Pkcs9AtExtendedCertificateAttributes = new(Pkcs9 + ".9");

    public static readonly DerObjectIdentifier Pkcs9AtSigningDescription = new(Pkcs9 + ".13");
    public static readonly DerObjectIdentifier Pkcs9AtExtensionRequest = new(Pkcs9 + ".14");
    public static readonly DerObjectIdentifier Pkcs9AtSmimeCapabilities = new(Pkcs9 + ".15");

    public static readonly DerObjectIdentifier IdSmime = new(Pkcs9 + ".16");

    public static readonly DerObjectIdentifier Pkcs9AtFriendlyName = new(Pkcs9 + ".20");
    public static readonly DerObjectIdentifier Pkcs9AtLocalKeyID = new(Pkcs9 + ".21");

    [Obsolete("Use X509Certificate instead")]
    public static readonly DerObjectIdentifier X509CertType = new(Pkcs9 + ".22.1");

    public static readonly DerObjectIdentifier X509Certificate = new(CertTypes + ".1");
    public static readonly DerObjectIdentifier SdsiCertificate = new(CertTypes + ".2");
    public static readonly DerObjectIdentifier X509Crl = new(CrlTypes + ".1");

    public static readonly DerObjectIdentifier IdAlg = IdSmime.Branch("3");

    public static readonly DerObjectIdentifier IdAlgEsdh = IdAlg.Branch("5");
    public static readonly DerObjectIdentifier IdAlgCms3DesWrap = IdAlg.Branch("6");
    public static readonly DerObjectIdentifier IdAlgCmsRC2Wrap = IdAlg.Branch("7");
    public static readonly DerObjectIdentifier IdAlgPwriKek = IdAlg.Branch("9");
    public static readonly DerObjectIdentifier IdAlgSsdh = IdAlg.Branch("10");

    /*
     * <pre>
     * -- RSA-KEM Key Transport Algorithm
     *
     * id-rsa-kem OID ::= {
     *      iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1)
     *      pkcs-9(9) smime(16) alg(3) 14
     *   }
     * </pre>
     */
    public static readonly DerObjectIdentifier IdRsaKem = IdAlg.Branch("14");

    /**
     * <pre>
     *     id-alg-AEADChaCha20Poly1305 OBJECT IDENTIFIER ::=
     *     { iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1)
     *     pkcs9(9) smime(16) alg(3) 18 }
     *     AEADChaCha20Poly1305Nonce ::= OCTET STRING (SIZE(12))
     * </pre>
     */
    public static readonly DerObjectIdentifier IdAlgAeadChaCha20Poly1305 = IdAlg.Branch("18");

    //
    // SMIME capability sub oids.
    //
    public static readonly DerObjectIdentifier PreferSignedData = Pkcs9AtSmimeCapabilities.Branch("1");
    public static readonly DerObjectIdentifier CannotDecryptAny = Pkcs9AtSmimeCapabilities.Branch("2");
    public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = Pkcs9AtSmimeCapabilities.Branch("3");

    //
    // other SMIME attributes
    //
    public static readonly DerObjectIdentifier IdAAReceiptRequest = IdSmime.Branch("2.1");

    public static readonly DerObjectIdentifier IdCTAuthData = new(IdCT + ".2");
    public static readonly DerObjectIdentifier IdCTTstInfo = new(IdCT + ".4");
    public static readonly DerObjectIdentifier IdCTCompressedData = new(IdCT + ".9");
    public static readonly DerObjectIdentifier IdCTAuthEnvelopedData = new(IdCT + ".23");
    public static readonly DerObjectIdentifier IdCTTimestampedData = new(IdCT + ".31");

    public static readonly DerObjectIdentifier IdCtiEtsProofOfOrigin = new(IdCti + ".1");
    public static readonly DerObjectIdentifier IdCtiEtsProofOfReceipt = new(IdCti + ".2");
    public static readonly DerObjectIdentifier IdCtiEtsProofOfDelivery = new(IdCti + ".3");
    public static readonly DerObjectIdentifier IdCtiEtsProofOfSender = new(IdCti + ".4");
    public static readonly DerObjectIdentifier IdCtiEtsProofOfApproval = new(IdCti + ".5");
    public static readonly DerObjectIdentifier IdCtiEtsProofOfCreation = new(IdCti + ".6");
    public static readonly DerObjectIdentifier IdAAOid = new(IdAA);

    public static readonly DerObjectIdentifier
        IdAAContentHint = new(IdAA + ".4"); // See RFC 2634

    public static readonly DerObjectIdentifier IdAAMsgSigDigest = new(IdAA + ".5");
    public static readonly DerObjectIdentifier IdAAContentReference = new(IdAA + ".10");

    /*
    * id-aa-encrypKeyPref OBJECT IDENTIFIER ::= {id-aa 11}
    *
    */
    public static readonly DerObjectIdentifier IdAAEncrypKeyPref = new(IdAA + ".11");
    public static readonly DerObjectIdentifier IdAASigningCertificate = new(IdAA + ".12");
    public static readonly DerObjectIdentifier IdAASigningCertificateV2 = new(IdAA + ".47");

    public static readonly DerObjectIdentifier
        IdAAContentIdentifier = new(IdAA + ".7"); // See RFC 2634

    /*
     * RFC 3126
     */
    public static readonly DerObjectIdentifier IdAASignatureTimeStampToken = new(IdAA + ".14");

    public static readonly DerObjectIdentifier IdAAEtsSigPolicyID = new(IdAA + ".15");
    public static readonly DerObjectIdentifier IdAAEtsCommitmentType = new(IdAA + ".16");
    public static readonly DerObjectIdentifier IdAAEtsSignerLocation = new(IdAA + ".17");
    public static readonly DerObjectIdentifier IdAAEtsSignerAttr = new(IdAA + ".18");
    public static readonly DerObjectIdentifier IdAAEtsOtherSigCert = new(IdAA + ".19");
    public static readonly DerObjectIdentifier IdAAEtsContentTimestamp = new(IdAA + ".20");
    public static readonly DerObjectIdentifier IdAAEtsCertificateRefs = new(IdAA + ".21");
    public static readonly DerObjectIdentifier IdAAEtsRevocationRefs = new(IdAA + ".22");
    public static readonly DerObjectIdentifier IdAAEtsCertValues = new(IdAA + ".23");
    public static readonly DerObjectIdentifier IdAAEtsRevocationValues = new(IdAA + ".24");
    public static readonly DerObjectIdentifier IdAAEtsEscTimeStamp = new(IdAA + ".25");
    public static readonly DerObjectIdentifier IdAAEtsCertCrlTimestamp = new(IdAA + ".26");
    public static readonly DerObjectIdentifier IdAAEtsArchiveTimestamp = new(IdAA + ".27");

    /**
     * PKCS#9: 1.2.840.113549.1.9.16.2.37 -
     * <a href="https://tools.ietf.org/html/rfc4108#section-2.2.5">RFC 4108</a>
     */
    public static readonly DerObjectIdentifier IdAADecryptKeyID = IdAAOid.Branch("37");

    /**
     * PKCS#9: 1.2.840.113549.1.9.16.2.38 -
     * <a href="https://tools.ietf.org/html/rfc4108#section-2.2.6">RFC 4108</a>
     */
    public static readonly DerObjectIdentifier IdAAImplCryptoAlgs = IdAAOid.Branch("38");

    /**
     * PKCS#9: 1.2.840.113549.1.9.16.2.54
     * <a href="https://tools.ietf.org/html/rfc7030">RFC7030</a>
     */
    public static readonly DerObjectIdentifier IdAAAsymmDecryptKeyID = IdAAOid.Branch("54");

    /**
     * PKCS#9: 1.2.840.113549.1.9.16.2.43
     * <a href="https://tools.ietf.org/html/rfc7030">RFC7030</a>
     */
    public static readonly DerObjectIdentifier IdAAImplCompressAlgs = IdAAOid.Branch("43");

    /**
     * PKCS#9: 1.2.840.113549.1.9.16.2.40
     * <a href="https://tools.ietf.org/html/rfc7030">RFC7030</a>
     */
    public static readonly DerObjectIdentifier IdAACommunityIdentifiers = IdAAOid.Branch("40");

    [Obsolete("Use 'IdAAEtsSigPolicyID' instead")]
    public static readonly DerObjectIdentifier IdAASigPolicyID = IdAAEtsSigPolicyID;

    [Obsolete("Use 'IdAAEtsCommitmentType' instead")]
    public static readonly DerObjectIdentifier IdAACommitmentType = IdAAEtsCommitmentType;

    [Obsolete("Use 'IdAAEtsSignerLocation' instead")]
    public static readonly DerObjectIdentifier IdAASignerLocation = IdAAEtsSignerLocation;

    [Obsolete("Use 'IdAAEtsOtherSigCert' instead")]
    public static readonly DerObjectIdentifier IdAAOtherSigCert = IdAAEtsOtherSigCert;

    public static readonly DerObjectIdentifier IdSpqEtsUri = new(IdSpq + ".1");
    public static readonly DerObjectIdentifier IdSpqEtsUNotice = new(IdSpq + ".2");

    public static readonly DerObjectIdentifier KeyBag = new(BagTypes + ".1");
    public static readonly DerObjectIdentifier Pkcs8ShroudedKeyBag = new(BagTypes + ".2");
    public static readonly DerObjectIdentifier CertBag = new(BagTypes + ".3");
    public static readonly DerObjectIdentifier CrlBag = new(BagTypes + ".4");
    public static readonly DerObjectIdentifier SecretBag = new(BagTypes + ".5");
    public static readonly DerObjectIdentifier SafeContentsBag = new(BagTypes + ".6");

    public static readonly DerObjectIdentifier
        PbeWithShaAnd128BitRC4 = new(Pkcs12PbeIds + ".1");

    public static readonly DerObjectIdentifier PbeWithShaAnd40BitRC4 = new(Pkcs12PbeIds + ".2");

    public static readonly DerObjectIdentifier PbeWithShaAnd3KeyTripleDesCbc = new(Pkcs12PbeIds + ".3");

    public static readonly DerObjectIdentifier PbeWithShaAnd2KeyTripleDesCbc = new(Pkcs12PbeIds + ".4");

    public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC2Cbc = new(Pkcs12PbeIds + ".5");

    public static readonly DerObjectIdentifier PbewithShaAnd40BitRC2Cbc = new(Pkcs12PbeIds + ".6");
}