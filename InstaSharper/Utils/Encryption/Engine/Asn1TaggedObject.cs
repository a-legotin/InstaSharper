using System;

namespace InstaSharper.Utils.Encryption.Engine;

/**
     * ASN.1 TaggedObject - in ASN.1 notation this is any object preceded by
     * a [n] where n is some number - these are assumed to follow the construction
     * rules (as with sequences).
     */
internal abstract class Asn1TaggedObject
    : Asn1Object, Asn1TaggedObjectParser
{
//        internal bool           empty;
    internal bool explicitly = true;
    internal Asn1Encodable obj;

    internal int tagNo;

    /**
         * @param tagNo the tag number for this object.
         * @param obj the tagged object.
         */
    protected Asn1TaggedObject(
        int tagNo,
        Asn1Encodable obj)
    {
        explicitly = true;
        this.tagNo = tagNo;
        this.obj = obj;
    }

    /**
         * @param explicitly true if the object is explicitly tagged.
         * @param tagNo the tag number for this object.
         * @param obj the tagged object.
         */
    protected Asn1TaggedObject(
        bool explicitly,
        int tagNo,
        Asn1Encodable obj)
    {
        // IAsn1Choice marker interface 'insists' on explicit tagging
        this.explicitly = explicitly;
        this.tagNo = tagNo;
        this.obj = obj;
    }

    public int TagNo => tagNo;

    /**
		* Return the object held in this tagged object as a parser assuming it has
		* the type of the passed in tag. If the object doesn't have a parser
		* associated with it, the base object is returned.
		*/
    public IAsn1Convertible GetObjectParser(
        int tag,
        bool isExplicit)
    {
        switch (tag)
        {
            case Asn1Tags.Set:
                return Asn1Set.GetInstance(this, isExplicit).Parser;
            case Asn1Tags.Sequence:
                return Asn1Sequence.GetInstance(this, isExplicit).Parser;
            case Asn1Tags.OctetString:
                return Asn1OctetString.GetInstance(this, isExplicit).Parser;
        }

        if (isExplicit) return GetObject();

        throw Platform.CreateNotImplementedException("implicit tagging for tag: " + tag);
    }

    internal static bool IsConstructed(bool isExplicit,
                                       Asn1Object obj)
    {
        if (isExplicit || obj is Asn1Sequence || obj is Asn1Set)
            return true;
        var tagged = obj as Asn1TaggedObject;
        if (tagged == null)
            return false;
        return IsConstructed(tagged.IsExplicit(), tagged.GetObject());
    }

    public static Asn1TaggedObject GetInstance(
        Asn1TaggedObject obj,
        bool explicitly)
    {
        if (explicitly) return GetInstance(obj.GetObject());

        throw new ArgumentException("implicitly tagged tagged object");
    }

    public static Asn1TaggedObject GetInstance(
        object obj)
    {
        if (obj == null || obj is Asn1TaggedObject) return (Asn1TaggedObject)obj;

        throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
    }

    protected override bool Asn1Equals(
        Asn1Object asn1Object)
    {
        var other = asn1Object as Asn1TaggedObject;

        if (other == null)
            return false;

        return tagNo == other.tagNo
//				&& this.empty == other.empty
               && explicitly == other.explicitly // TODO Should this be part of equality?
               && Equals(GetObject(), other.GetObject());
    }

    protected override int Asn1GetHashCode()
    {
        var code = tagNo.GetHashCode();

        // TODO: actually this is wrong - the problem is that a re-encoded
        // object may end up with a different hashCode due to implicit
        // tagging. As implicit tagging is ambiguous if a sequence is involved
        // it seems the only correct method for both equals and hashCode is to
        // compare the encodings...
//			code ^= explicitly.GetHashCode();

        if (obj != null) code ^= obj.GetHashCode();

        return code;
    }

    /**
     * return whether or not the object may be explicitly tagged.
     * <p>
     *     Note: if the object has been read from an input stream, the only
     *     time you can be sure if isExplicit is returning the true state of
     *     affairs is if it returns false. An implicitly tagged object may appear
     *     to be explicitly tagged, so you need to understand the context under
     *     which the reading was done as well, see GetObject below.
     * </p>
     */
    public bool IsExplicit()
    {
        return explicitly;
    }

    public bool IsEmpty()
    {
        return false;
        //empty;
    }

    /**
     * return whatever was following the tag.
     * <p>
     *     Note: tagged objects are generally context dependent if you're
     *     trying to extract a tagged object you should be going via the
     *     appropriate GetInstance method.
     * </p>
     */
    public Asn1Object GetObject()
    {
        if (obj != null) return obj.ToAsn1Object();

        return null;
    }

    public override string ToString()
    {
        return "[" + tagNo + "]" + obj;
    }
}