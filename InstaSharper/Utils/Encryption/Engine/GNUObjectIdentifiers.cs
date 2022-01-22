namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class GnuObjectIdentifiers
{
    public static readonly DerObjectIdentifier Gnu = new("1.3.6.1.4.1.11591.1"); // GNU Radius

    public static readonly DerObjectIdentifier
        GnuPG = new("1.3.6.1.4.1.11591.2"); // GnuPG (Ägypten)

    public static readonly DerObjectIdentifier
        Notation = new("1.3.6.1.4.1.11591.2.1"); // notation

    public static readonly DerObjectIdentifier
        PkaAddress = new("1.3.6.1.4.1.11591.2.1.1"); // pkaAddress

    public static readonly DerObjectIdentifier
        GnuRadar = new("1.3.6.1.4.1.11591.3"); // GNU Radar

    public static readonly DerObjectIdentifier
        DigestAlgorithm = new("1.3.6.1.4.1.11591.12"); // digestAlgorithm

    public static readonly DerObjectIdentifier
        Tiger192 = new("1.3.6.1.4.1.11591.12.2"); // TIGER/192

    public static readonly DerObjectIdentifier
        EncryptionAlgorithm = new("1.3.6.1.4.1.11591.13"); // encryptionAlgorithm

    public static readonly DerObjectIdentifier
        Serpent = new("1.3.6.1.4.1.11591.13.2"); // Serpent

    public static readonly DerObjectIdentifier
        Serpent128Ecb = new("1.3.6.1.4.1.11591.13.2.1"); // Serpent-128-ECB

    public static readonly DerObjectIdentifier
        Serpent128Cbc = new("1.3.6.1.4.1.11591.13.2.2"); // Serpent-128-CBC

    public static readonly DerObjectIdentifier
        Serpent128Ofb = new("1.3.6.1.4.1.11591.13.2.3"); // Serpent-128-OFB

    public static readonly DerObjectIdentifier
        Serpent128Cfb = new("1.3.6.1.4.1.11591.13.2.4"); // Serpent-128-CFB

    public static readonly DerObjectIdentifier
        Serpent192Ecb = new("1.3.6.1.4.1.11591.13.2.21"); // Serpent-192-ECB

    public static readonly DerObjectIdentifier
        Serpent192Cbc = new("1.3.6.1.4.1.11591.13.2.22"); // Serpent-192-CBC

    public static readonly DerObjectIdentifier
        Serpent192Ofb = new("1.3.6.1.4.1.11591.13.2.23"); // Serpent-192-OFB

    public static readonly DerObjectIdentifier
        Serpent192Cfb = new("1.3.6.1.4.1.11591.13.2.24"); // Serpent-192-CFB

    public static readonly DerObjectIdentifier
        Serpent256Ecb = new("1.3.6.1.4.1.11591.13.2.41"); // Serpent-256-ECB

    public static readonly DerObjectIdentifier
        Serpent256Cbc = new("1.3.6.1.4.1.11591.13.2.42"); // Serpent-256-CBC

    public static readonly DerObjectIdentifier
        Serpent256Ofb = new("1.3.6.1.4.1.11591.13.2.43"); // Serpent-256-OFB

    public static readonly DerObjectIdentifier
        Serpent256Cfb = new("1.3.6.1.4.1.11591.13.2.44"); // Serpent-256-CFB

    public static readonly DerObjectIdentifier
        Crc = new("1.3.6.1.4.1.11591.14"); // CRC algorithms

    public static readonly DerObjectIdentifier Crc32 = new("1.3.6.1.4.1.11591.14.1"); // CRC 32

    /**
     * 1.3.6.1.4.1.11591.15 - ellipticCurve
     */
    public static readonly DerObjectIdentifier EllipticCurve = new("1.3.6.1.4.1.11591.15");

    public static readonly DerObjectIdentifier Ed25519 = EllipticCurve.Branch("1");
}