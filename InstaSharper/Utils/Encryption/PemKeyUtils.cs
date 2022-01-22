using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace InstaSharper.Utils.Encryption;

internal class PemKeyUtils
{
    public static RSACryptoServiceProvider GetRSAProviderFromPemString(string cert)
    {
        return GetRSACryptoServiceProvide(GetByteArrayFromCert(cert));
    }

    private static byte[] GetByteArrayFromCert(string cert)
    {
        const string header = "-----BEGIN PUBLIC KEY-----";
        const string footer = "-----END PUBLIC KEY-----";
        if (!cert.StartsWith(header) || !cert.EndsWith(footer))
            return null;

        var sb = new StringBuilder(cert.Trim());
        sb.Replace(header, "").Replace(footer, "");

        var publicKey = sb.ToString().Trim(); //get string after removing leading/trailing whitespace

        try
        {
            return Convert.FromBase64String(publicKey);
        }
        catch (FormatException)
        {
            //if can't b64 decode, data is not valid
            return null;
        }
    }

    private static RSACryptoServiceProvider GetRSACryptoServiceProvide(byte[] x509Key)
    {
        byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        using (var ms = new MemoryStream(x509Key))
        {
            using (var br = new BinaryReader(ms))
            {
                try
                {
                    var twobytes = br.ReadUInt16();
                    switch (twobytes)
                    {
                        case 0x8130:
                            br.ReadByte(); //advance 1 byte
                            break;
                        case 0x8230:
                            br.ReadInt16(); //advance 2 bytes
                            break;
                        default:
                            return null;
                    }

                    var seq = br.ReadBytes(15);
                    if (!CompareBytearrays(seq, seqOid))
                        return null;

                    twobytes = br.ReadUInt16();
                    if (twobytes == 0x8103
                       ) //data read as little endian order (actual data order for Bit String is 03 81)
                        br.ReadByte(); //advance 1 byte
                    else if (twobytes == 0x8203)
                        br.ReadInt16(); //advance 2 bytes
                    else
                        return null;

                    var bt = br.ReadByte();
                    if (bt != 0x00) //expect null byte next
                        return null;

                    twobytes = br.ReadUInt16();
                    if (twobytes == 0x8130
                       ) //data read as little endian order (actual data order for Sequence is 30 81)
                        br.ReadByte(); //advance 1 byte
                    else if (twobytes == 0x8230)
                        br.ReadInt16(); //advance 2 bytes
                    else
                        return null;

                    twobytes = br.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102
                       ) //data read as little endian order (actual data order for Integer is 02 81)
                    {
                        lowbyte = br.ReadByte(); // read next bytes which is bytes in modulus
                    }
                    else if (twobytes == 0x8202)
                    {
                        highbyte = br.ReadByte(); //advance 2 bytes
                        lowbyte = br.ReadByte();
                    }
                    else
                    {
                        return null;
                    }

                    byte[] modint =
                    {
                        lowbyte, highbyte, 0x00, 0x00
                    }; //reverse byte order since asn.1 key uses big endian order
                    var modsize = BitConverter.ToInt32(modint, 0);

                    var firstbyte = br.ReadByte();
                    br.BaseStream.Seek(-1, SeekOrigin.Current);

                    if (firstbyte == 0x00)
                    {
                        //if first byte (highest order) of modulus is zero, don't include it
                        br.ReadByte(); //skip this null byte
                        modsize -= 1; //reduce modulus buffer size by 1
                    }

                    var modulus = br.ReadBytes(modsize); //read the modulus bytes

                    if (br.ReadByte() != 0x02) //expect an Integer for the exponent data
                        return null;
                    int expbytes =
                        br.ReadByte(); // should only need one byte for actual exponent data (for all useful values)
                    var exponent = br.ReadBytes(expbytes);

                    var rsa = new RSACryptoServiceProvider();
                    var rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }

    private static bool CompareBytearrays(byte[] a,
                                          byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        var i = 0;
        foreach (var c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }

        return true;
    }
}