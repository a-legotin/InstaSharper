using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine;

/**
	 * BER TaggedObject - in ASN.1 notation this is any object preceded by
	 * a [n] where n is some number - these are assumed to follow the construction
	 * rules (as with sequences).
	 */
internal class BerTaggedObject
    : DerTaggedObject
{
    /**
		 * @param tagNo the tag number for this object.
		 * @param obj the tagged object.
		 */
    public BerTaggedObject(
        int tagNo,
        Asn1Encodable obj)
        : base(tagNo, obj)
    {
    }

    /**
		 * @param explicitly true if an explicitly tagged object.
		 * @param tagNo the tag number for this object.
		 * @param obj the tagged object.
		 */
    public BerTaggedObject(
        bool explicitly,
        int tagNo,
        Asn1Encodable obj)
        : base(explicitly, tagNo, obj)
    {
    }

    /**
		 * create an implicitly tagged object that contains a zero
		 * length sequence.
		 */
    public BerTaggedObject(
        int tagNo)
        : base(false, tagNo, BerSequence.Empty)
    {
    }

    internal override void Encode(
        DerOutputStream derOut)
    {
        if (derOut is Asn1OutputStream || derOut is BerOutputStream)
        {
            derOut.WriteTag(Asn1Tags.Constructed | Asn1Tags.Tagged, tagNo);
            derOut.WriteByte(0x80);

            if (!IsEmpty())
            {
                if (!explicitly)
                {
                    IEnumerable eObj;
                    if (obj is Asn1OctetString)
                    {
                        if (obj is BerOctetString)
                        {
                            eObj = (BerOctetString)obj;
                        }
                        else
                        {
                            var octs = (Asn1OctetString)obj;
                            eObj = new BerOctetString(octs.GetOctets());
                        }
                    }
                    else if (obj is Asn1Sequence)
                    {
                        eObj = (Asn1Sequence)obj;
                    }
                    else if (obj is Asn1Set)
                    {
                        eObj = (Asn1Set)obj;
                    }
                    else
                    {
                        throw Platform.CreateNotImplementedException(Platform.GetTypeName(obj));
                    }

                    foreach (Asn1Encodable o in eObj) derOut.WriteObject(o);
                }
                else
                {
                    derOut.WriteObject(obj);
                }
            }

            derOut.WriteByte(0x00);
            derOut.WriteByte(0x00);
        }
        else
        {
            base.Encode(derOut);
        }
    }
}