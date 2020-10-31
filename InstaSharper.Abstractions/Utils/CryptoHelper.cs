using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace InstaSharper.Abstractions.Utils
{
    internal class CryptoHelper
    {
        public static string ByteToString(byte[] buff) =>
            buff.Aggregate("", (current, item) => current + item.ToString("X2"));

        public static string Base64Encode(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Base64Decode(string base64EncodedData) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));

        public static string CalculateMd5(string message)
        {
            var utF8 = Encoding.UTF8;
            using var md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(utF8.GetBytes(message))).Replace("-", "").ToLower();
        }
    }
}