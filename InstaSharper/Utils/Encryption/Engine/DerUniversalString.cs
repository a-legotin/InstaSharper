using System;
using System.Text;

namespace InstaSharper.Utils.Encryption.Engine
{
    /**
     * Der UniversalString object.
     */
    internal class DerUniversalString
        : DerStringBase
    {
        private static readonly char[] table =
            {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        private readonly byte[] str;

        /**
         * basic constructor - byte encoded string.
         */
        public DerUniversalString(
            byte[] str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            this.str = str;
        }

        /**
         * return a Universal string from the passed in object.
         *
         * @exception ArgumentException if the object cannot be converted.
         */
        public static DerUniversalString GetInstance(
            object obj)
        {
            if (obj == null || obj is DerUniversalString)
            {
                return (DerUniversalString) obj;
            }

            throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
        }

        /**
         * return a Universal string from a tagged object.
         *
         * @param obj the tagged object holding the object we want
         * @param explicitly true if the object is meant to be explicitly
         *              tagged false otherwise.
         * @exception ArgumentException if the tagged object cannot
         *               be converted.
         */
        public static DerUniversalString GetInstance(
            Asn1TaggedObject obj,
            bool isExplicit)
        {
            var o = obj.GetObject();

            if (isExplicit || o is DerUniversalString)
            {
                return GetInstance(o);
            }

            return new DerUniversalString(Asn1OctetString.GetInstance(o).GetOctets());
        }

        public override string GetString()
        {
            var buffer = new StringBuilder("#");
            var enc = GetDerEncoded();

            for (var i = 0; i != enc.Length; i++)
            {
                uint ubyte = enc[i];
                buffer.Append(table[(ubyte >> 4) & 0xf]);
                buffer.Append(table[enc[i] & 0xf]);
            }

            return buffer.ToString();
        }

        public byte[] GetOctets() => (byte[]) str.Clone();

        internal override void Encode(
            DerOutputStream derOut)
        {
            derOut.WriteEncoded(Asn1Tags.UniversalString, str);
        }

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            var other = asn1Object as DerUniversalString;

            if (other == null)
                return false;

//			return this.GetString().Equals(other.GetString());
            return Arrays.AreEqual(str, other.str);
        }
    }
}