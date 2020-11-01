using System;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class DerGraphicString
        : DerStringBase
    {
        private readonly byte[] mString;

        /**
         * basic constructor - with bytes.
         * @param string the byte encoding of the characters making up the string.
         */
        public DerGraphicString(byte[] encoding) => mString = Arrays.Clone(encoding);

        /**
         * return a Graphic String from the passed in object
         *
         * @param obj a DerGraphicString or an object that can be converted into one.
         * @exception IllegalArgumentException if the object cannot be converted.
         * @return a DerGraphicString instance, or null.
         */
        public static DerGraphicString GetInstance(object obj)
        {
            if (obj == null || obj is DerGraphicString)
            {
                return (DerGraphicString) obj;
            }

            if (obj is byte[])
            {
                try
                {
                    return (DerGraphicString) FromByteArray((byte[]) obj);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("encoding error in GetInstance: " + e, "obj");
                }
            }

            throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
        }

        /**
         * return a Graphic String from a tagged object.
         *
         * @param obj the tagged object holding the object we want
         * @param explicit true if the object is meant to be explicitly
         *              tagged false otherwise.
         * @exception IllegalArgumentException if the tagged object cannot
         *               be converted.
         * @return a DerGraphicString instance, or null.
         */
        public static DerGraphicString GetInstance(Asn1TaggedObject obj, bool isExplicit)
        {
            var o = obj.GetObject();

            if (isExplicit || o is DerGraphicString)
            {
                return GetInstance(o);
            }

            return new DerGraphicString(((Asn1OctetString) o).GetOctets());
        }

        public override string GetString() => Strings.FromByteArray(mString);

        public byte[] GetOctets() => Arrays.Clone(mString);

        internal override void Encode(DerOutputStream derOut)
        {
            derOut.WriteEncoded(Asn1Tags.GraphicString, mString);
        }

        protected override int Asn1GetHashCode() => Arrays.GetHashCode(mString);

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            var other = asn1Object as DerGraphicString;

            if (other == null)
                return false;

            return Arrays.AreEqual(mString, other.mString);
        }
    }
}