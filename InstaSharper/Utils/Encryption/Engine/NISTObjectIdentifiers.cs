namespace InstaSharper.Utils.Encryption.Engine
{
    public sealed class NistObjectIdentifiers
    {
        //
        // NIST
        //     iso/itu(2) joint-assign(16) us(840) organization(1) gov(101) csor(3)

        //
        // nistalgorithms(4)
        //
        internal static readonly DerObjectIdentifier NistAlgorithm = new DerObjectIdentifier("2.16.840.1.101.3.4");

        internal static readonly DerObjectIdentifier HashAlgs = NistAlgorithm.Branch("2");

        internal static readonly DerObjectIdentifier IdSha256 = HashAlgs.Branch("1");
        internal static readonly DerObjectIdentifier IdSha384 = HashAlgs.Branch("2");
        internal static readonly DerObjectIdentifier IdSha512 = HashAlgs.Branch("3");
        internal static readonly DerObjectIdentifier IdSha224 = HashAlgs.Branch("4");
        internal static readonly DerObjectIdentifier IdSha512_224 = HashAlgs.Branch("5");
        internal static readonly DerObjectIdentifier IdSha512_256 = HashAlgs.Branch("6");
        internal static readonly DerObjectIdentifier IdSha3_224 = HashAlgs.Branch("7");
        internal static readonly DerObjectIdentifier IdSha3_256 = HashAlgs.Branch("8");
        internal static readonly DerObjectIdentifier IdSha3_384 = HashAlgs.Branch("9");
        internal static readonly DerObjectIdentifier IdSha3_512 = HashAlgs.Branch("10");
        internal static readonly DerObjectIdentifier IdShake128 = HashAlgs.Branch("11");
        internal static readonly DerObjectIdentifier IdShake256 = HashAlgs.Branch("12");
        internal static readonly DerObjectIdentifier IdHMacWithSha3_224 = HashAlgs.Branch("13");
        internal static readonly DerObjectIdentifier IdHMacWithSha3_256 = HashAlgs.Branch("14");
        internal static readonly DerObjectIdentifier IdHMacWithSha3_384 = HashAlgs.Branch("15");
        internal static readonly DerObjectIdentifier IdHMacWithSha3_512 = HashAlgs.Branch("16");
        internal static readonly DerObjectIdentifier IdShake128Len = HashAlgs.Branch("17");
        internal static readonly DerObjectIdentifier IdShake256Len = HashAlgs.Branch("18");
        internal static readonly DerObjectIdentifier IdKmacWithShake128 = HashAlgs.Branch("19");
        internal static readonly DerObjectIdentifier IdKmacWithShake256 = HashAlgs.Branch("20");

        internal static readonly DerObjectIdentifier Aes = new DerObjectIdentifier(NistAlgorithm + ".1");

        internal static readonly DerObjectIdentifier IdAes128Ecb = new DerObjectIdentifier(Aes + ".1");
        internal static readonly DerObjectIdentifier IdAes128Cbc = new DerObjectIdentifier(Aes + ".2");
        internal static readonly DerObjectIdentifier IdAes128Ofb = new DerObjectIdentifier(Aes + ".3");
        internal static readonly DerObjectIdentifier IdAes128Cfb = new DerObjectIdentifier(Aes + ".4");
        internal static readonly DerObjectIdentifier IdAes128Wrap = new DerObjectIdentifier(Aes + ".5");
        internal static readonly DerObjectIdentifier IdAes128Gcm = new DerObjectIdentifier(Aes + ".6");
        internal static readonly DerObjectIdentifier IdAes128Ccm = new DerObjectIdentifier(Aes + ".7");

        internal static readonly DerObjectIdentifier IdAes192Ecb = new DerObjectIdentifier(Aes + ".21");
        internal static readonly DerObjectIdentifier IdAes192Cbc = new DerObjectIdentifier(Aes + ".22");
        internal static readonly DerObjectIdentifier IdAes192Ofb = new DerObjectIdentifier(Aes + ".23");
        internal static readonly DerObjectIdentifier IdAes192Cfb = new DerObjectIdentifier(Aes + ".24");
        internal static readonly DerObjectIdentifier IdAes192Wrap = new DerObjectIdentifier(Aes + ".25");
        internal static readonly DerObjectIdentifier IdAes192Gcm = new DerObjectIdentifier(Aes + ".26");
        internal static readonly DerObjectIdentifier IdAes192Ccm = new DerObjectIdentifier(Aes + ".27");

        internal static readonly DerObjectIdentifier IdAes256Ecb = new DerObjectIdentifier(Aes + ".41");
        internal static readonly DerObjectIdentifier IdAes256Cbc = new DerObjectIdentifier(Aes + ".42");
        internal static readonly DerObjectIdentifier IdAes256Ofb = new DerObjectIdentifier(Aes + ".43");
        internal static readonly DerObjectIdentifier IdAes256Cfb = new DerObjectIdentifier(Aes + ".44");
        internal static readonly DerObjectIdentifier IdAes256Wrap = new DerObjectIdentifier(Aes + ".45");
        internal static readonly DerObjectIdentifier IdAes256Gcm = new DerObjectIdentifier(Aes + ".46");
        internal static readonly DerObjectIdentifier IdAes256Ccm = new DerObjectIdentifier(Aes + ".47");

        //
        // signatures
        //
        internal static readonly DerObjectIdentifier IdDsaWithSha2 = new DerObjectIdentifier(NistAlgorithm + ".3");

        internal static readonly DerObjectIdentifier DsaWithSha224 = new DerObjectIdentifier(IdDsaWithSha2 + ".1");
        internal static readonly DerObjectIdentifier DsaWithSha256 = new DerObjectIdentifier(IdDsaWithSha2 + ".2");
        internal static readonly DerObjectIdentifier DsaWithSha384 = new DerObjectIdentifier(IdDsaWithSha2 + ".3");
        internal static readonly DerObjectIdentifier DsaWithSha512 = new DerObjectIdentifier(IdDsaWithSha2 + ".4");

        /** 2.16.840.1.101.3.4.3.5 */
        internal static readonly DerObjectIdentifier IdDsaWithSha3_224 = new DerObjectIdentifier(IdDsaWithSha2 + ".5");

        /** 2.16.840.1.101.3.4.3.6 */
        internal static readonly DerObjectIdentifier IdDsaWithSha3_256 = new DerObjectIdentifier(IdDsaWithSha2 + ".6");

        /** 2.16.840.1.101.3.4.3.7 */
        internal static readonly DerObjectIdentifier IdDsaWithSha3_384 = new DerObjectIdentifier(IdDsaWithSha2 + ".7");

        /** 2.16.840.1.101.3.4.3.8 */
        internal static readonly DerObjectIdentifier IdDsaWithSha3_512 = new DerObjectIdentifier(IdDsaWithSha2 + ".8");

        // ECDSA with SHA-3
        /** 2.16.840.1.101.3.4.3.9 */
        internal static readonly DerObjectIdentifier
            IdEcdsaWithSha3_224 = new DerObjectIdentifier(IdDsaWithSha2 + ".9");

        /** 2.16.840.1.101.3.4.3.10 */
        internal static readonly DerObjectIdentifier IdEcdsaWithSha3_256 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".10");

        /** 2.16.840.1.101.3.4.3.11 */
        internal static readonly DerObjectIdentifier IdEcdsaWithSha3_384 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".11");

        /** 2.16.840.1.101.3.4.3.12 */
        internal static readonly DerObjectIdentifier IdEcdsaWithSha3_512 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".12");

        // RSA PKCS #1 v1.5 Signature with SHA-3 family.
        /** 2.16.840.1.101.3.4.3.9 */
        internal static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_224 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".13");

        /** 2.16.840.1.101.3.4.3.10 */
        internal static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_256 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".14");

        /** 2.16.840.1.101.3.4.3.11 */
        internal static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_384 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".15");

        /** 2.16.840.1.101.3.4.3.12 */
        internal static readonly DerObjectIdentifier IdRsassaPkcs1V15WithSha3_512 =
            new DerObjectIdentifier(IdDsaWithSha2 + ".16");

        private NistObjectIdentifiers()
        {
        }
    }
}