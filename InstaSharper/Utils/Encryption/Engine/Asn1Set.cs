using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
#if PORTABLE
using System.Collections.Generic;
using System.Linq;
#endif

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class Asn1Set
        : Asn1Object, IEnumerable
    {
        // NOTE: Only non-readonly to support LazyDerSet
        internal Asn1Encodable[] elements;

        /**
         * return an ASN1Set from the given object.
         *
         * @param obj the object we want converted.
         * @exception ArgumentException if the object cannot be converted.
         */
        public static Asn1Set GetInstance(
            object obj)
        {
            if (obj == null || obj is Asn1Set) return (Asn1Set)obj;

            if (obj is Asn1SetParser) return GetInstance(((Asn1SetParser)obj).ToAsn1Object());

            if (obj is byte[])
                try
                {
                    return GetInstance(FromByteArray((byte[])obj));
                }
                catch (IOException e)
                {
                    throw new ArgumentException("failed to construct set from byte[]: " + e.Message);
                }

            if (obj is Asn1Encodable)
            {
                var primitive = ((Asn1Encodable)obj).ToAsn1Object();

                if (primitive is Asn1Set) return (Asn1Set)primitive;
            }

            throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
        }

        /**
         * Return an ASN1 set from a tagged object. There is a special
         * case here, if an object appears to have been explicitly tagged on
         * reading but we were expecting it to be implicitly tagged in the
         * normal course of events it indicates that we lost the surrounding
         * set - so we need to add it back (this will happen if the tagged
         * object is a sequence that contains other sequences). If you are
         * dealing with implicitly tagged sets you really
         * <b>should</b>
         * be using this method.
         * 
         * @param obj the tagged object.
         * @param explicitly true if the object is meant to be explicitly tagged
         * false otherwise.
         * @exception ArgumentException if the tagged object cannot
         * be converted.
         */
        public static Asn1Set GetInstance(
            Asn1TaggedObject obj,
            bool explicitly)
        {
            var inner = obj.GetObject();

            if (explicitly)
            {
                if (!obj.IsExplicit())
                    throw new ArgumentException("object implicit - explicit expected.");

                return (Asn1Set)inner;
            }

            //
            // constructed object which appears to be explicitly tagged
            // and it's really implicit means we have to add the
            // surrounding sequence.
            //
            if (obj.IsExplicit()) return new DerSet(inner);

            if (inner is Asn1Set) return (Asn1Set)inner;

            //
            // in this case the parser returns a sequence, convert it
            // into a set.
            //
            if (inner is Asn1Sequence)
            {
                var v = new Asn1EncodableVector();
                var s = (Asn1Sequence)inner;

                foreach (Asn1Encodable ae in s) v.Add(ae);

                // TODO Should be able to construct set directly from sequence?
                return new DerSet(v, false);
            }

            throw new ArgumentException("Unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
        }

        protected internal Asn1Set()
        {
            elements = Asn1EncodableVector.EmptyElements;
        }

        protected internal Asn1Set(Asn1Encodable element)
        {
            if (null == element)
                throw new ArgumentNullException("element");

            elements = new[] { element };
        }

        protected internal Asn1Set(params Asn1Encodable[] elements)
        {
            if (Arrays.IsNullOrContainsNull(elements))
                throw new NullReferenceException("'elements' cannot be null, or contain null");

            this.elements = Asn1EncodableVector.CloneElements(elements);
        }

        protected internal Asn1Set(Asn1EncodableVector elementVector)
        {
            if (null == elementVector)
                throw new ArgumentNullException("elementVector");

            elements = elementVector.TakeElements();
        }

        public virtual IEnumerator GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        /**
         * return the object at the set position indicated by index.
         *
         * @param index the set number (starting at zero) of the object
         * @return the object at the set position indicated by index.
         */
        public virtual Asn1Encodable this[int index] => elements[index];

        public virtual int Count => elements.Length;

        public virtual Asn1Encodable[] ToArray()
        {
            return Asn1EncodableVector.CloneElements(elements);
        }

        private class Asn1SetParserImpl
            : Asn1SetParser
        {
            private readonly int max;
            private readonly Asn1Set outer;
            private int index;

            public Asn1SetParserImpl(
                Asn1Set outer)
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

            public virtual Asn1Object ToAsn1Object()
            {
                return outer;
            }
        }

        public Asn1SetParser Parser => new Asn1SetParserImpl(this);

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
            var that = asn1Object as Asn1Set;
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

        protected internal void Sort()
        {
            if (elements.Length < 2)
                return;

#if PORTABLE
            this.elements = elements
                .Cast<Asn1Encodable>()
                .Select(a => new { Item = a, Key = a.GetEncoded(Asn1Encodable.Der) })
                .OrderBy(t => t.Key, new DerComparer())
                .Select(t => t.Item)
                .ToArray();
#else
            var count = elements.Length;
            var keys = new byte[count][];
            for (var i = 0; i < count; ++i) keys[i] = elements[i].GetEncoded(Der);

            Array.Sort(keys, elements, new DerComparer());
#endif
        }

        public override string ToString()
        {
            return CollectionUtilities.ToString(elements);
        }

#if PORTABLE
        private class DerComparer
            : IComparer<byte[]>
        {
            public int Compare(byte[] x, byte[] y)
            {
                byte[] a = x, b = y;
#else
        private class DerComparer
            : IComparer
        {
            public int Compare(object x,
                               object y)
            {
                byte[] a = (byte[])x, b = (byte[])y;
#endif
                Debug.Assert(a.Length >= 2 && b.Length >= 2);

                /*
                 * NOTE: Set elements in DER encodings are ordered first according to their tags (class and
                 * number); the CONSTRUCTED bit is not part of the tag.
                 * 
                 * For SET-OF, this is unimportant. All elements have the same tag and DER requires them to
                 * either all be in constructed form or all in primitive form, according to that tag. The
                 * elements are effectively ordered according to their content octets.
                 * 
                 * For SET, the elements will have distinct tags, and each will be in constructed or
                 * primitive form accordingly. Failing to ignore the CONSTRUCTED bit could therefore lead to
                 * ordering inversions.
                 */
                var a0 = a[0] & ~Asn1Tags.Constructed;
                var b0 = b[0] & ~Asn1Tags.Constructed;
                if (a0 != b0)
                    return a0 < b0 ? -1 : 1;

                var len = Math.Min(a.Length, b.Length);
                for (var i = 1; i < len; ++i)
                {
                    byte ai = a[i], bi = b[i];
                    if (ai != bi)
                        return ai < bi ? -1 : 1;
                }

                Debug.Assert(a.Length == b.Length);
                return 0;
            }
        }
    }
}