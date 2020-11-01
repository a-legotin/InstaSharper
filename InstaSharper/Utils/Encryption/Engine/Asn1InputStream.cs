using System;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    /**
     * a general purpose ASN.1 decoder - note: this class differs from the
     * others in that it returns null after it has read the last object in
     * the stream. If an ASN.1 Null is encountered a Der/BER Null object is
     * returned.
     */
    internal class Asn1InputStream
        : FilterStream
    {
        private readonly int limit;

        private readonly byte[][] tmpBuffers;

        public Asn1InputStream(
            Stream inputStream)
            : this(inputStream, FindLimit(inputStream))
        {
        }

        /**
         * Create an ASN1InputStream where no DER object will be longer than limit.
         *
         * @param input stream containing ASN.1 encoded data.
         * @param limit maximum size of a DER encoded object.
         */
        public Asn1InputStream(
            Stream inputStream,
            int limit)
            : base(inputStream)
        {
            this.limit = limit;
            tmpBuffers = new byte[16][];
        }

        /**
         * Create an ASN1InputStream based on the input byte array. The length of DER objects in
         * the stream is automatically limited to the length of the input array.
         *
         * @param input array containing ASN.1 encoded data.
         */
        public Asn1InputStream(
            byte[] input)
            : this(new MemoryStream(input, false), input.Length)
        {
        }

        internal virtual int Limit => limit;

        internal static int FindLimit(Stream input)
        {
            if (input is LimitedInputStream)
                return ((LimitedInputStream) input).Limit;

            if (input is Asn1InputStream)
                return ((Asn1InputStream) input).Limit;

            if (input is MemoryStream)
            {
                var mem = (MemoryStream) input;
                return (int) (mem.Length - mem.Position);
            }

            return int.MaxValue;
        }

        /**
        * build an object given its tag and the number of bytes to construct it from.
        */
        private Asn1Object BuildObject(
            int tag,
            int tagNo,
            int length)
        {
            var isConstructed = (tag & Asn1Tags.Constructed) != 0;

            var defIn = new DefiniteLengthInputStream(s, length, limit);

            if ((tag & Asn1Tags.Application) != 0)
            {
                return new DerApplicationSpecific(isConstructed, tagNo, defIn.ToArray());
            }

            if ((tag & Asn1Tags.Tagged) != 0)
            {
                return new Asn1StreamParser(defIn).ReadTaggedObject(isConstructed, tagNo);
            }

            if (isConstructed)
            {
                // TODO There are other tags that may be constructed (e.g. BitString)
                switch (tagNo)
                {
                    case Asn1Tags.OctetString:
                    {
                        //
                        // yes, people actually do this...
                        //
                        var v = ReadVector(defIn);
                        var strings = new Asn1OctetString[v.Count];

                        for (var i = 0; i != strings.Length; i++)
                        {
                            var asn1Obj = v[i];
                            if (!(asn1Obj is Asn1OctetString))
                            {
                                throw new Asn1Exception("unknown object encountered in constructed OCTET STRING: "
                                                        + Platform.GetTypeName(asn1Obj));
                            }

                            strings[i] = (Asn1OctetString) asn1Obj;
                        }

                        return new BerOctetString(strings);
                    }
                    case Asn1Tags.Sequence:
                        return CreateDerSequence(defIn);
                    case Asn1Tags.Set:
                        return CreateDerSet(defIn);
                    case Asn1Tags.External:
                        return new DerExternal(ReadVector(defIn));
                    default:
                        throw new IOException("unknown tag " + tagNo + " encountered");
                }
            }

            return CreatePrimitiveDerObject(tagNo, defIn, tmpBuffers);
        }

        internal virtual Asn1EncodableVector ReadVector(DefiniteLengthInputStream dIn)
        {
            if (dIn.Remaining < 1)
                return new Asn1EncodableVector(0);

            var subStream = new Asn1InputStream(dIn);
            var v = new Asn1EncodableVector();
            Asn1Object o;
            while ((o = subStream.ReadObject()) != null)
            {
                v.Add(o);
            }

            return v;
        }

        internal virtual DerSequence CreateDerSequence(
            DefiniteLengthInputStream dIn) =>
            DerSequence.FromVector(ReadVector(dIn));

        internal virtual DerSet CreateDerSet(
            DefiniteLengthInputStream dIn) =>
            DerSet.FromVector(ReadVector(dIn), false);

        public Asn1Object ReadObject()
        {
            var tag = ReadByte();
            if (tag <= 0)
            {
                if (tag == 0)
                    throw new IOException("unexpected end-of-contents marker");

                return null;
            }

            //
            // calculate tag number
            //
            var tagNo = ReadTagNumber(s, tag);

            var isConstructed = (tag & Asn1Tags.Constructed) != 0;

            //
            // calculate length
            //
            var length = ReadLength(s, limit, false);

            if (length < 0) // indefinite-length method
            {
                if (!isConstructed)
                    throw new IOException("indefinite-length primitive encoding encountered");

                var indIn = new IndefiniteLengthInputStream(s, limit);
                var sp = new Asn1StreamParser(indIn, limit);

                if ((tag & Asn1Tags.Application) != 0)
                {
                    return new BerApplicationSpecificParser(tagNo, sp).ToAsn1Object();
                }

                if ((tag & Asn1Tags.Tagged) != 0)
                {
                    return new BerTaggedObjectParser(true, tagNo, sp).ToAsn1Object();
                }

                // TODO There are other tags that may be constructed (e.g. BitString)
                switch (tagNo)
                {
                    case Asn1Tags.OctetString:
                        return new BerOctetStringParser(sp).ToAsn1Object();
                    case Asn1Tags.Sequence:
                        return new BerSequenceParser(sp).ToAsn1Object();
                    case Asn1Tags.Set:
                        return new BerSetParser(sp).ToAsn1Object();
                    case Asn1Tags.External:
                        return new DerExternalParser(sp).ToAsn1Object();
                    default:
                        throw new IOException("unknown BER object encountered");
                }
            }

            try
            {
                return BuildObject(tag, tagNo, length);
            }
            catch (ArgumentException e)
            {
                throw new Asn1Exception("corrupted stream detected", e);
            }
        }

        internal static int ReadTagNumber(
            Stream s,
            int tag)
        {
            var tagNo = tag & 0x1f;

            //
            // with tagged object tag number is bottom 5 bits, or stored at the start of the content
            //
            if (tagNo == 0x1f)
            {
                tagNo = 0;

                var b = s.ReadByte();

                // X.690-0207 8.1.2.4.2
                // "c) bits 7 to 1 of the first subsequent octet shall not all be zero."
                if ((b & 0x7f) == 0) // Note: -1 will pass
                    throw new IOException("corrupted stream - invalid high tag number found");

                while ((b >= 0) && ((b & 0x80) != 0))
                {
                    tagNo |= (b & 0x7f);
                    tagNo <<= 7;
                    b = s.ReadByte();
                }

                if (b < 0)
                    throw new EndOfStreamException("EOF found inside tag value.");

                tagNo |= (b & 0x7f);
            }

            return tagNo;
        }

        internal static int ReadLength(Stream s, int limit, bool isParsing)
        {
            var length = s.ReadByte();
            if (length < 0)
                throw new EndOfStreamException("EOF found when length expected");

            if (length == 0x80)
                return -1;      // indefinite-length encoding

            if (length > 127)
            {
                var size = length & 0x7f;

                // Note: The invalid long form "0xff" (see X.690 8.1.3.5c) will be caught here
                if (size > 4)
                    throw new IOException("DER length more than 4 bytes: " + size);

                length = 0;
                for (var i = 0; i < size; i++)
                {
                    var next = s.ReadByte();

                    if (next < 0)
                        throw new EndOfStreamException("EOF found reading length");

                    length = (length << 8) + next;
                }

                if (length < 0)
                    throw new IOException("corrupted stream - negative length found");

                if (length >= limit && !isParsing)   // after all we must have read at least 1 byte
                    throw new IOException("corrupted stream - out of bounds length found: " + length + " >= " + limit);
            }

            return length;
        }

        private static byte[] GetBuffer(DefiniteLengthInputStream defIn, byte[][] tmpBuffers)
        {
            var len = defIn.Remaining;
            if (len >= tmpBuffers.Length)
            {
                return defIn.ToArray();
            }

            var buf = tmpBuffers[len];
            if (buf == null)
            {
                buf = tmpBuffers[len] = new byte[len];
            }

            defIn.ReadAllIntoByteArray(buf);

            return buf;
        }

        private static char[] GetBmpCharBuffer(DefiniteLengthInputStream defIn)
        {
            var remainingBytes = defIn.Remaining;
            if (0 != (remainingBytes & 1))
                throw new IOException("malformed BMPString encoding encountered");

            var str = new char[remainingBytes / 2];
            var stringPos = 0;

            var buf = new byte[8];
            while (remainingBytes >= 8)
            {
                if (Streams.ReadFully(defIn, buf, 0, 8) != 8)
                    throw new EndOfStreamException("EOF encountered in middle of BMPString");

                str[stringPos    ] = (char) ((buf[0] << 8) | (buf[1] & 0xFF));
                str[stringPos + 1] = (char) ((buf[2] << 8) | (buf[3] & 0xFF));
                str[stringPos + 2] = (char) ((buf[4] << 8) | (buf[5] & 0xFF));
                str[stringPos + 3] = (char) ((buf[6] << 8) | (buf[7] & 0xFF));
                stringPos += 4;
                remainingBytes -= 8;
            }

            if (remainingBytes > 0)
            {
                if (Streams.ReadFully(defIn, buf, 0, remainingBytes) != remainingBytes)
                    throw new EndOfStreamException("EOF encountered in middle of BMPString");

                var bufPos = 0;
                do
                {
                    var b1 = buf[bufPos++] << 8;
                    var b2 = buf[bufPos++] & 0xFF;
                    str[stringPos++] = (char) (b1 | b2);
                } while (bufPos < remainingBytes);
            }

            if (0 != defIn.Remaining || str.Length != stringPos)
                throw new InvalidOperationException();

            return str;
        }

        internal static Asn1Object CreatePrimitiveDerObject(
            int                         tagNo,
            DefiniteLengthInputStream   defIn,
            byte[][]                    tmpBuffers)
        {
            switch (tagNo)
            {
                case Asn1Tags.BmpString:
                    return new DerBmpString(GetBmpCharBuffer(defIn));
                case Asn1Tags.Boolean:
                    return DerBoolean.FromOctetString(GetBuffer(defIn, tmpBuffers));
                case Asn1Tags.Enumerated:
                    return DerEnumerated.FromOctetString(GetBuffer(defIn, tmpBuffers));
                case Asn1Tags.ObjectIdentifier:
                    return DerObjectIdentifier.FromOctetString(GetBuffer(defIn, tmpBuffers));
            }

            var bytes = defIn.ToArray();

            switch (tagNo)
            {
                case Asn1Tags.BitString:
                    return DerBitString.FromAsn1Octets(bytes);
                case Asn1Tags.GeneralizedTime:
                    return new DerGeneralizedTime(bytes);
                case Asn1Tags.GeneralString:
                    return new DerGeneralString(bytes);
                case Asn1Tags.GraphicString:
                    return new DerGraphicString(bytes);
                case Asn1Tags.IA5String:
                    return new DerIA5String(bytes);
                case Asn1Tags.Integer:
                    return new DerInteger(bytes, false);
                case Asn1Tags.Null:
                    return DerNull.Instance;   // actual content is ignored (enforce 0 length?)
                case Asn1Tags.NumericString:
                    return new DerNumericString(bytes);
                case Asn1Tags.OctetString:
                    return new DerOctetString(bytes);
                case Asn1Tags.PrintableString:
                    return new DerPrintableString(bytes);
                case Asn1Tags.T61String:
                    return new DerT61String(bytes);
                case Asn1Tags.UniversalString:
                    return new DerUniversalString(bytes);
                case Asn1Tags.UtcTime:
                    return new DerUtcTime(bytes);
                case Asn1Tags.Utf8String:
                    return new DerUtf8String(bytes);
                case Asn1Tags.VideotexString:
                    return new DerVideotexString(bytes);
                case Asn1Tags.VisibleString:
                    return new DerVisibleString(bytes);
                default:
                    throw new IOException("unknown tag " + tagNo + " encountered");
            }
        }
    }
}