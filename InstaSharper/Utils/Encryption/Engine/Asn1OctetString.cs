using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class Asn1OctetString
        : Asn1Object, Asn1OctetStringParser
    {
        internal byte[] str;

        /**
         * @param string the octets making up the octet string.
         */
        internal Asn1OctetString(
            byte[] str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            this.str = str;
        }

        public Asn1OctetStringParser Parser => this;

        public Stream GetOctetStream() => new MemoryStream(str, false);

        /**
         * return an Octet string from a tagged object.
         *
         * @param obj the tagged object holding the object we want.
         * @param explicitly true if the object is meant to be explicitly
         *              tagged false otherwise.
         * @exception ArgumentException if the tagged object cannot
         *              be converted.
         */
        public static Asn1OctetString GetInstance(
            Asn1TaggedObject obj,
            bool isExplicit)
        {
            var o = obj.GetObject();

            if (isExplicit || o is Asn1OctetString)
            {
                return GetInstance(o);
            }

            return BerOctetString.FromSequence(Asn1Sequence.GetInstance(o));
        }

        /**
         * return an Octet string from the given object.
         *
         * @param obj the object we want converted.
         * @exception ArgumentException if the object cannot be converted.
         */
        public static Asn1OctetString GetInstance(object obj)
        {
            if (obj == null || obj is Asn1OctetString)
            {
                return (Asn1OctetString) obj;
            }

            if (obj is byte[])
            {
                try
                {
                    return GetInstance(FromByteArray((byte[]) obj));
                }
                catch (IOException e)
                {
                    throw new ArgumentException("failed to construct OCTET STRING from byte[]: " + e.Message);
                }
            }
            // TODO: this needs to be deleted in V2

            if (obj is Asn1TaggedObject)
            {
                return GetInstance(((Asn1TaggedObject) obj).GetObject());
            }

            if (obj is Asn1Encodable)
            {
                var primitive = ((Asn1Encodable) obj).ToAsn1Object();

                if (primitive is Asn1OctetString)
                {
                    return (Asn1OctetString) primitive;
                }
            }

            throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
        }

        public virtual byte[] GetOctets() => str;

        protected override int Asn1GetHashCode() => Arrays.GetHashCode(GetOctets());

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            var other = asn1Object as DerOctetString;

            if (other == null)
                return false;

            return Arrays.AreEqual(GetOctets(), other.GetOctets());
        }

        public override string ToString() => "#" + Hex.ToHexString(str);
    }
}