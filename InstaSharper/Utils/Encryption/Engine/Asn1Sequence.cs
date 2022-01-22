using System;
using System.Collections;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine;

internal abstract class Asn1Sequence
    : Asn1Object, IEnumerable
{
    // NOTE: Only non-readonly to support LazyDerSequence
    internal Asn1Encodable[] elements;

    protected internal Asn1Sequence()
    {
        elements = Asn1EncodableVector.EmptyElements;
    }

    protected internal Asn1Sequence(Asn1Encodable element)
    {
        if (null == element)
            throw new ArgumentNullException("element");

        elements = new[] { element };
    }

    protected internal Asn1Sequence(params Asn1Encodable[] elements)
    {
        if (Arrays.IsNullOrContainsNull(elements))
            throw new NullReferenceException("'elements' cannot be null, or contain null");

        this.elements = Asn1EncodableVector.CloneElements(elements);
    }

    protected internal Asn1Sequence(Asn1EncodableVector elementVector)
    {
        if (null == elementVector)
            throw new ArgumentNullException("elementVector");

        elements = elementVector.TakeElements();
    }

    public virtual Asn1SequenceParser Parser => new Asn1SequenceParserImpl(this);

    /**
         * return the object at the sequence position indicated by index.
         *
         * @param index the sequence number (starting at zero) of the object
         * @return the object at the sequence position indicated by index.
         */
    public virtual Asn1Encodable this[int index] => elements[index];

    public virtual int Count => elements.Length;

    public virtual IEnumerator GetEnumerator()
    {
        return elements.GetEnumerator();
    }

    /**
         * return an Asn1Sequence from the given object.
         *
         * @param obj the object we want converted.
         * @exception ArgumentException if the object cannot be converted.
         */
    public static Asn1Sequence GetInstance(
        object obj)
    {
        if (obj == null || obj is Asn1Sequence) return (Asn1Sequence)obj;

        if (obj is Asn1SequenceParser) return GetInstance(((Asn1SequenceParser)obj).ToAsn1Object());

        if (obj is byte[])
            try
            {
                return GetInstance(FromByteArray((byte[])obj));
            }
            catch (IOException e)
            {
                throw new ArgumentException("failed to construct sequence from byte[]: " + e.Message);
            }

        if (obj is Asn1Encodable)
        {
            var primitive = ((Asn1Encodable)obj).ToAsn1Object();

            if (primitive is Asn1Sequence) return (Asn1Sequence)primitive;
        }

        throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
    }

    /**
     * Return an ASN1 sequence from a tagged object. There is a special
     * case here, if an object appears to have been explicitly tagged on
     * reading but we were expecting it to be implicitly tagged in the
     * normal course of events it indicates that we lost the surrounding
     * sequence - so we need to add it back (this will happen if the tagged
     * object is a sequence that contains other sequences). If you are
     * dealing with implicitly tagged sequences you really
     * <b>should</b>
     * be using this method.
     * 
     * @param obj the tagged object.
     * @param explicitly true if the object is meant to be explicitly tagged,
     * false otherwise.
     * @exception ArgumentException if the tagged object cannot
     * be converted.
     */
    public static Asn1Sequence GetInstance(
        Asn1TaggedObject obj,
        bool explicitly)
    {
        var inner = obj.GetObject();

        if (explicitly)
        {
            if (!obj.IsExplicit())
                throw new ArgumentException("object implicit - explicit expected.");

            return (Asn1Sequence)inner;
        }

        //
        // constructed object which appears to be explicitly tagged
        // when it should be implicit means we have to add the
        // surrounding sequence.
        //
        if (obj.IsExplicit())
        {
            if (obj is BerTaggedObject) return new BerSequence(inner);

            return new DerSequence(inner);
        }

        if (inner is Asn1Sequence) return (Asn1Sequence)inner;

        throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
    }

    public virtual Asn1Encodable[] ToArray()
    {
        return Asn1EncodableVector.CloneElements(elements);
    }

    protected override int Asn1GetHashCode()
    {
        //return Arrays.GetHashCode(elements);
        var i = elements.Length;
        var hc = i + 1;

        while (--i >= 0)
        {
            hc *= 257;
            hc ^= elements[i].ToAsn1Object().CallAsn1GetHashCode();
        }

        return hc;
    }

    protected override bool Asn1Equals(Asn1Object asn1Object)
    {
        var that = asn1Object as Asn1Sequence;
        if (null == that)
            return false;

        var count = Count;
        if (that.Count != count)
            return false;

        for (var i = 0; i < count; ++i)
        {
            var o1 = elements[i].ToAsn1Object();
            var o2 = that.elements[i].ToAsn1Object();

            if (o1 != o2 && !o1.CallAsn1Equals(o2))
                return false;
        }

        return true;
    }

    public override string ToString()
    {
        return CollectionUtilities.ToString(elements);
    }

    private class Asn1SequenceParserImpl
        : Asn1SequenceParser
    {
        private readonly int max;
        private readonly Asn1Sequence outer;
        private int index;

        public Asn1SequenceParserImpl(
            Asn1Sequence outer)
        {
            this.outer = outer;
            max = outer.Count;
        }

        public IAsn1Convertible ReadObject()
        {
            if (index == max)
                return null;

            var obj = outer[index++];

            if (obj is Asn1Sequence)
                return ((Asn1Sequence)obj).Parser;

            if (obj is Asn1Set)
                return ((Asn1Set)obj).Parser;

            // NB: Asn1OctetString implements Asn1OctetStringParser directly
//				if (obj is Asn1OctetString)
//					return ((Asn1OctetString)obj).Parser;

            return obj;
        }

        public Asn1Object ToAsn1Object()
        {
            return outer;
        }
    }
}