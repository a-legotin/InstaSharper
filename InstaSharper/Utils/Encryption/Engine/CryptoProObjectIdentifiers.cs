namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class CryptoProObjectIdentifiers
{
    // GOST Algorithms OBJECT IDENTIFIERS :
    // { iso(1) member-body(2) ru(643) rans(2) cryptopro(2)}
    public const string GostID = "1.2.643.2.2";

    public static readonly DerObjectIdentifier GostR3411 = new(GostID + ".9");
    public static readonly DerObjectIdentifier GostR3411Hmac = new(GostID + ".10");

    public static readonly DerObjectIdentifier GostR28147Cbc = new(GostID + ".21");

    public static readonly DerObjectIdentifier ID_Gost28147_89_CryptoPro_A_ParamSet = new(GostID + ".31.1");

    public static readonly DerObjectIdentifier GostR3410x94 = new(GostID + ".20");
    public static readonly DerObjectIdentifier GostR3410x2001 = new(GostID + ".19");

    public static readonly DerObjectIdentifier
        GostR3411x94WithGostR3410x94 = new(GostID + ".4");

    public static readonly DerObjectIdentifier GostR3411x94WithGostR3410x2001 = new(GostID + ".3");

    // { iso(1) member-body(2) ru(643) rans(2) cryptopro(2) hashes(30) }
    public static readonly DerObjectIdentifier GostR3411x94CryptoProParamSet = new(GostID + ".30.1");

    // { iso(1) member-body(2) ru(643) rans(2) cryptopro(2) signs(32) }
    public static readonly DerObjectIdentifier GostR3410x94CryptoProA = new(GostID + ".32.2");
    public static readonly DerObjectIdentifier GostR3410x94CryptoProB = new(GostID + ".32.3");
    public static readonly DerObjectIdentifier GostR3410x94CryptoProC = new(GostID + ".32.4");
    public static readonly DerObjectIdentifier GostR3410x94CryptoProD = new(GostID + ".32.5");

    // { iso(1) member-body(2) ru(643) rans(2) cryptopro(2) exchanges(33) }
    public static readonly DerObjectIdentifier
        GostR3410x94CryptoProXchA = new(GostID + ".33.1");

    public static readonly DerObjectIdentifier
        GostR3410x94CryptoProXchB = new(GostID + ".33.2");

    public static readonly DerObjectIdentifier
        GostR3410x94CryptoProXchC = new(GostID + ".33.3");

    //{ iso(1) member-body(2)ru(643) rans(2) cryptopro(2) ecc-signs(35) }
    public static readonly DerObjectIdentifier GostR3410x2001CryptoProA = new(GostID + ".35.1");
    public static readonly DerObjectIdentifier GostR3410x2001CryptoProB = new(GostID + ".35.2");
    public static readonly DerObjectIdentifier GostR3410x2001CryptoProC = new(GostID + ".35.3");

    // { iso(1) member-body(2) ru(643) rans(2) cryptopro(2) ecc-exchanges(36) }
    public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchA = new(GostID + ".36.0");

    public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchB = new(GostID + ".36.1");

    public static readonly DerObjectIdentifier GostElSgDH3410Default = new(GostID + ".36.0");
    public static readonly DerObjectIdentifier GostElSgDH3410x1 = new(GostID + ".36.1");
}