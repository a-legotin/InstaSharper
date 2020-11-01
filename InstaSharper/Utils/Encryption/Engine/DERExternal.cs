using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    /**
	* Class representing the DER-type External
	*/
    internal class DerExternal
        : Asn1Object
    {
        private int encoding;

        public DerExternal(
            Asn1EncodableVector vector)
        {
            var offset = 0;
            var enc = GetObjFromVector(vector, offset);
            if (enc is DerObjectIdentifier)
            {
                DirectReference = (DerObjectIdentifier) enc;
                offset++;
                enc = GetObjFromVector(vector, offset);
            }

            if (enc is DerInteger)
            {
                IndirectReference = (DerInteger) enc;
                offset++;
                enc = GetObjFromVector(vector, offset);
            }

            if (!(enc is Asn1TaggedObject))
            {
                DataValueDescriptor = enc;
                offset++;
                enc = GetObjFromVector(vector, offset);
            }

            if (vector.Count != offset + 1)
                throw new ArgumentException("input vector too large", "vector");

            if (!(enc is Asn1TaggedObject))
                throw new ArgumentException(
                    "No tagged object found in vector. Structure doesn't seem to be of type External", "vector");

            var obj = (Asn1TaggedObject) enc;

            // Use property accessor to include check on value
            Encoding = obj.TagNo;

            if (encoding < 0 || encoding > 2)
                throw new InvalidOperationException("invalid encoding value");

            ExternalContent = obj.GetObject();
        }

        /**
		* Creates a new instance of DerExternal
		* See X.690 for more informations about the meaning of these parameters
		* @param directReference The direct reference or <code>null</code> if not set.
		* @param indirectReference The indirect reference or <code>null</code> if not set.
		* @param dataValueDescriptor The data value descriptor or <code>null</code> if not set.
		* @param externalData The external data in its encoded form.
		*/
        public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference,
            Asn1Object dataValueDescriptor, DerTaggedObject externalData)
            : this(directReference, indirectReference, dataValueDescriptor, externalData.TagNo,
                externalData.ToAsn1Object())
        {
        }

        /**
		* Creates a new instance of DerExternal.
		* See X.690 for more informations about the meaning of these parameters
		* @param directReference The direct reference or <code>null</code> if not set.
		* @param indirectReference The indirect reference or <code>null</code> if not set.
		* @param dataValueDescriptor The data value descriptor or <code>null</code> if not set.
		* @param encoding The encoding to be used for the external data
		* @param externalData The external data
		*/
        public DerExternal(DerObjectIdentifier directReference, DerInteger indirectReference,
            Asn1Object dataValueDescriptor, int encoding, Asn1Object externalData)
        {
            DirectReference = directReference;
            IndirectReference = indirectReference;
            DataValueDescriptor = dataValueDescriptor;
            Encoding = encoding;
            ExternalContent = externalData.ToAsn1Object();
        }

        public Asn1Object DataValueDescriptor { get; set; }

        public DerObjectIdentifier DirectReference { get; set; }

        /**
		* The encoding of the content. Valid values are
		* <ul>
		* <li><code>0</code> single-ASN1-type</li>
		* <li><code>1</code> OCTET STRING</li>
		* <li><code>2</code> BIT STRING</li>
		* </ul>
		*/
        public int Encoding
        {
            get => encoding;
            set
            {
                if (encoding < 0 || encoding > 2)
                    throw new InvalidOperationException("invalid encoding value: " + encoding);

                encoding = value;
            }
        }

        public Asn1Object ExternalContent { get; set; }

        public DerInteger IndirectReference { get; set; }

        internal override void Encode(DerOutputStream derOut)
        {
            var ms = new MemoryStream();
            WriteEncodable(ms, DirectReference);
            WriteEncodable(ms, IndirectReference);
            WriteEncodable(ms, DataValueDescriptor);
            WriteEncodable(ms, new DerTaggedObject(Asn1Tags.External, ExternalContent));

            derOut.WriteEncoded(Asn1Tags.Constructed, Asn1Tags.External, ms.ToArray());
        }

        protected override int Asn1GetHashCode()
        {
            var ret = ExternalContent.GetHashCode();
            if (DirectReference != null)
            {
                ret ^= DirectReference.GetHashCode();
            }

            if (IndirectReference != null)
            {
                ret ^= IndirectReference.GetHashCode();
            }

            if (DataValueDescriptor != null)
            {
                ret ^= DataValueDescriptor.GetHashCode();
            }

            return ret;
        }

        protected override bool Asn1Equals(
            Asn1Object asn1Object)
        {
            if (this == asn1Object)
                return true;

            var other = asn1Object as DerExternal;

            if (other == null)
                return false;

            return Platform.Equals(DirectReference, other.DirectReference)
                   && Platform.Equals(IndirectReference, other.IndirectReference)
                   && Platform.Equals(DataValueDescriptor, other.DataValueDescriptor)
                   && ExternalContent.Equals(other.ExternalContent);
        }

        private static Asn1Object GetObjFromVector(Asn1EncodableVector v, int index)
        {
            if (v.Count <= index)
                throw new ArgumentException("too few objects in input vector", "v");

            return v[index].ToAsn1Object();
        }

        private static void WriteEncodable(MemoryStream ms, Asn1Encodable e)
        {
            if (e != null)
            {
                var bs = e.GetDerEncoded();
                ms.Write(bs, 0, bs.Length);
            }
        }
    }
}