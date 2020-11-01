using System;
using System.Collections;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class BerOctetString
        : DerOctetString, IEnumerable
    {
        private static readonly int DefaultChunkSize = 1000;

        private readonly int chunkSize;
        private readonly Asn1OctetString[] octs;

        [Obsolete("Will be removed")]
        public BerOctetString(IEnumerable e)
            : this(ToOctetStringArray(e))
        {
        }

        public BerOctetString(byte[] str)
            : this(str, DefaultChunkSize)
        {
        }

        public BerOctetString(Asn1OctetString[] octs)
            : this(octs, DefaultChunkSize)
        {
        }

        public BerOctetString(byte[] str, int chunkSize)
            : this(str, null, chunkSize)
        {
        }

        public BerOctetString(Asn1OctetString[] octs, int chunkSize)
            : this(ToBytes(octs), octs, chunkSize)
        {
        }

        private BerOctetString(byte[] str, Asn1OctetString[] octs, int chunkSize)
            : base(str)
        {
            this.octs = octs;
            this.chunkSize = chunkSize;
        }

        /**
         * return the DER octets that make up this string.
         */
        public IEnumerator GetEnumerator()
        {
            if (octs == null)
                return new ChunkEnumerator(str, chunkSize);

            return octs.GetEnumerator();
        }

        public static BerOctetString FromSequence(Asn1Sequence seq)
        {
            var count = seq.Count;
            var v = new Asn1OctetString[count];
            for (var i = 0; i < count; ++i)
            {
                v[i] = GetInstance(seq[i]);
            }

            return new BerOctetString(v);
        }

        private static byte[] ToBytes(Asn1OctetString[] octs)
        {
            var bOut = new MemoryStream();
            foreach (var o in octs)
            {
                var octets = o.GetOctets();
                bOut.Write(octets, 0, octets.Length);
            }

            return bOut.ToArray();
        }

        private static Asn1OctetString[] ToOctetStringArray(IEnumerable e)
        {
            var list = Platform.CreateArrayList(e);

            var count = list.Count;
            var v = new Asn1OctetString[count];
            for (var i = 0; i < count; ++i)
            {
                v[i] = GetInstance(list[i]);
            }

            return v;
        }

        [Obsolete("Use GetEnumerator() instead")]
        public IEnumerator GetObjects() => GetEnumerator();

        internal override void Encode(
            DerOutputStream derOut)
        {
            if (derOut is Asn1OutputStream || derOut is BerOutputStream)
            {
                derOut.WriteByte(Asn1Tags.Constructed | Asn1Tags.OctetString);

                derOut.WriteByte(0x80);

                //
                // write out the octet array
                //
                foreach (Asn1OctetString oct in this)
                {
                    derOut.WriteObject(oct);
                }

                derOut.WriteByte(0x00);
                derOut.WriteByte(0x00);
            }
            else
            {
                base.Encode(derOut);
            }
        }

        private class ChunkEnumerator
            : IEnumerator
        {
            private readonly int chunkSize;
            private readonly byte[] octets;

            private DerOctetString currentChunk  ;
            private int nextChunkPos  ;

            internal ChunkEnumerator(byte[] octets, int chunkSize)
            {
                this.octets = octets;
                this.chunkSize = chunkSize;
            }

            public object Current
            {
                get
                {
                    if (null == currentChunk)
                        throw new InvalidOperationException();

                    return currentChunk;
                }
            }

            public bool MoveNext()
            {
                if (nextChunkPos >= octets.Length)
                {
                    currentChunk = null;
                    return false;
                }

                var length = Math.Min(octets.Length - nextChunkPos, chunkSize);
                var chunk = new byte[length];
                Array.Copy(octets, nextChunkPos, chunk, 0, length);
                currentChunk = new DerOctetString(chunk);
                nextChunkPos += length;
                return true;
            }

            public void Reset()
            {
                currentChunk = null;
                nextChunkPos = 0;
            }
        }
    }
}