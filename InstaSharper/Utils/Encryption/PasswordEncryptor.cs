using System;
using System.Linq;
using System.Text;
using InstaSharper.Utils.Encryption.Engine;

namespace InstaSharper.Utils.Encryption
{
    internal class PasswordEncryptor : IPasswordEncryptor
    {
        public string EncryptPassword(string password, string pubKey, string pubKeyId, long time)
        {
            var secureRandom = new SecureRandom();
            var randomKeyBytes = new byte[32];
            var bufferBytes = new byte[12];
            secureRandom.NextBytes(randomKeyBytes, 0, randomKeyBytes.Length);
            secureRandom.NextBytes(bufferBytes, 0, bufferBytes.Length);
            var associatedData = Encoding.UTF8.GetBytes(time.ToString());
            var publicKey = Encoding.UTF8.GetString(Convert.FromBase64String(pubKey));

            using var provider = PemKeyUtils.GetRSAProviderFromPemString(publicKey.Trim());
            var encryptedKey = provider.Encrypt(randomKeyBytes, false);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(randomKeyBytes), 128, bufferBytes, associatedData);
            cipher.Init(true, parameters);
            var ciphertext = new byte[cipher.GetOutputSize(passwordBytes.Length)];
            var len = cipher.ProcessBytes(passwordBytes, 0, passwordBytes.Length, ciphertext, 0);
            cipher.DoFinal(ciphertext, len);

            var con = new byte[passwordBytes.Length];
            Buffer.BlockCopy(ciphertext, 0, con, 0, passwordBytes.Length);
            ciphertext = con;
            var mac = cipher.GetMac();
            var buffersSize = BitConverter.GetBytes(Convert.ToInt16(encryptedKey.Length));
            var encryptionBytes = BitConverter.GetBytes(Convert.ToUInt16(pubKeyId));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(encryptionBytes);
            encryptionBytes[0] = 1;
            return Convert.ToBase64String(encryptionBytes.Concat(bufferBytes).Concat(buffersSize).Concat(encryptedKey)
                .Concat(mac).Concat(ciphertext).ToArray());
        }
    }
}