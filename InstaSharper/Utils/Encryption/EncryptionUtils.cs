using System;
using System.Linq;
using System.Text;
using InstaSharper.Utils.Encryption;
using InstaSharper.Utils.Encryption.Lib;

namespace InstaSharper.Utils.Encryption
{
    internal static class EncryptionUtils
    {
        internal static string EncryptPassword(string password, string pubKey, string pubKeyId, long time)
            {
                var secureRandom = new SecureRandom();
                var randKey = new byte[32];
                var iv = new byte[12];
                secureRandom.NextBytes(randKey, 0, randKey.Length);
                secureRandom.NextBytes(iv, 0, iv.Length);
                var associatedData = Encoding.UTF8.GetBytes(time.ToString());
                var pubKEY = Encoding.UTF8.GetString(Convert.FromBase64String(pubKey));
                byte[] encryptedKey;
                using (var rdr = PemKeyUtils.GetRSAProviderFromPemString(pubKEY.Trim()))
                    encryptedKey = rdr.Encrypt(randKey, false);

                var plaintext = Encoding.UTF8.GetBytes(password);

                var cipher = new GcmBlockCipher(new AesEngine());
                var parameters = new AeadParameters(new KeyParameter(randKey), 128, iv, associatedData);
                cipher.Init(true, parameters);

                var ciphertext = new byte[cipher.GetOutputSize(plaintext.Length)];
                var len = cipher.ProcessBytes(plaintext, 0, plaintext.Length, ciphertext, 0);
                cipher.DoFinal(ciphertext, len);

                var con = new byte[plaintext.Length];
                Buffer.BlockCopy(ciphertext, 0, con, 0, plaintext.Length);
                ciphertext = con;
                var tag = cipher.GetMac();

                var buffersSize = BitConverter.GetBytes(Convert.ToInt16(encryptedKey.Length));

                var encKeyIdBytes = BitConverter.GetBytes(Convert.ToUInt16(pubKeyId));
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(encKeyIdBytes);
                encKeyIdBytes[0] = 1;
                return Convert.ToBase64String(encKeyIdBytes.Concat(iv).Concat(buffersSize).Concat(encryptedKey)
                    .Concat(tag).Concat(ciphertext).ToArray());
            }
        }
}