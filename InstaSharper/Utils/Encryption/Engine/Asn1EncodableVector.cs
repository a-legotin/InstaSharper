using System;
using System.Collections;

namespace InstaSharper.Utils.Encryption.Engine
{
    /**
     * Mutable class for building ASN.1 constructed objects such as SETs or SEQUENCEs.
     */
    internal class Asn1EncodableVector
        : IEnumerable
    {
        private const int DefaultCapacity = 10;
        internal static readonly Asn1Encodable[] EmptyElements = new Asn1Encodable[0];
        private bool copyOnWrite;

        private Asn1Encodable[] elements;

        public Asn1EncodableVector()
            : this(DefaultCapacity)
        {
        }

        public Asn1EncodableVector(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentException("must not be negative", "initialCapacity");

            elements = (initialCapacity == 0) ? EmptyElements : new Asn1Encodable[initialCapacity];
            Count = 0;
            copyOnWrite = false;
        }

        public Asn1EncodableVector(params Asn1Encodable[] v)
            : this()
        {
            Add(v);
        }

        public Asn1Encodable this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException(index + " >= " + Count);

                return elements[index];
            }
        }

        public int Count { get; private set; }

        public IEnumerator GetEnumerator() => CopyElements().GetEnumerator();

        public static Asn1EncodableVector FromEnumerable(IEnumerable e)
        {
            var v = new Asn1EncodableVector();
            foreach (Asn1Encodable obj in e)
            {
                v.Add(obj);
            }

            return v;
        }

        public void Add(Asn1Encodable element)
        {
            if (null == element)
                throw new ArgumentNullException("element");

            var capacity = elements.Length;
            var minCapacity = Count + 1;
            if ((minCapacity > capacity) | copyOnWrite)
            {
                Reallocate(minCapacity);
            }

            elements[Count] = element;
            Count = minCapacity;
        }

        public void Add(params Asn1Encodable[] objs)
        {
            foreach (var obj in objs)
            {
                Add(obj);
            }
        }

        public void AddOptional(params Asn1Encodable[] objs)
        {
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    if (obj != null)
                    {
                        Add(obj);
                    }
                }
            }
        }

        public void AddOptionalTagged(bool isExplicit, int tagNo, Asn1Encodable obj)
        {
            if (null != obj)
            {
                Add(new DerTaggedObject(isExplicit, tagNo, obj));
            }
        }

        public void AddAll(Asn1EncodableVector other)
        {
            if (null == other)
                throw new ArgumentNullException("other");

            var otherElementCount = other.Count;
            if (otherElementCount < 1)
                return;

            var capacity = elements.Length;
            var minCapacity = Count + otherElementCount;
            if ((minCapacity > capacity) | copyOnWrite)
            {
                Reallocate(minCapacity);
            }

            var i = 0;
            do
            {
                var otherElement = other[i];
                if (null == otherElement)
                    throw new NullReferenceException("'other' elements cannot be null");

                elements[Count + i] = otherElement;
            } while (++i < otherElementCount);

            Count = minCapacity;
        }

        internal Asn1Encodable[] CopyElements()
        {
            if (0 == Count)
                return EmptyElements;

            var copy = new Asn1Encodable[Count];
            Array.Copy(elements, 0, copy, 0, Count);
            return copy;
        }

        internal Asn1Encodable[] TakeElements()
        {
            if (0 == Count)
                return EmptyElements;

            if (elements.Length == Count)
            {
                copyOnWrite = true;
                return elements;
            }

            var copy = new Asn1Encodable[Count];
            Array.Copy(elements, 0, copy, 0, Count);
            return copy;
        }

        private void Reallocate(int minCapacity)
        {
            var oldCapacity = elements.Length;
            var newCapacity = Math.Max(oldCapacity, minCapacity + (minCapacity >> 1));

            var copy = new Asn1Encodable[newCapacity];
            Array.Copy(elements, 0, copy, 0, Count);

            elements = copy;
            copyOnWrite = false;
        }

        internal static Asn1Encodable[] CloneElements(Asn1Encodable[] elements) =>
            elements.Length < 1 ? EmptyElements : (Asn1Encodable[]) elements.Clone();
    }
}