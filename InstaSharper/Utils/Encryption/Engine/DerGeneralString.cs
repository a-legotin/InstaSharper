using System;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class DerGeneralString
        : DerStringBase
    {
        private readonly string str;

        public DerGeneralString(
            byte[] str)
            : this(Strings.FromAsciiByteArray(str))
        {
        }

        public DerGeneralString(
            string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            this.str = str;
        }

        public static DerGeneralString GetInstance(
            object obj)
        {
            if (obj == null || obj is DerGeneralString)
            {
                return (DerGeneralString) obj;
            }

            throw new ArgumentException("illegal object in GetInstance: "
                                        + Platform.GetTypeName(obj));
        }

        public static DerGeneralString GetInstance(
            Asn1TaggedObject obj,
            bool isExplicit)
        {
            var o = obj.GetObject();

            if (isExplicit || o is DerGeneralString)
            {
                return GetInstance(o);
            }

            return new DerGeneralString(((Asn1OctetString) o).GetOctets());
        }

        public override string GetString() => str;

        public byte[] GetOctets() => Strings.ToAsciiByteArray(str);

        internal override void Encode(
            DerOutputStream derOut)
        {
            derOut.WriteEncoded(Asn1Tags.GeneralString, GetOctets());
        }

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            var other = asn1Object as DerGeneralString;

            if (other == null)
                return false;

            return str.Equals(other.str);
        }
    }
}