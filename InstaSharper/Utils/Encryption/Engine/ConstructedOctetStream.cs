using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal class ConstructedOctetStream
        : BaseInputStream
    {
        private readonly Asn1StreamParser _parser;
        private Stream _currentStream;

        private bool _first = true;

        internal ConstructedOctetStream(
            Asn1StreamParser parser) =>
            _parser = parser;

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_currentStream == null)
            {
                if (!_first)
                    return 0;

                var next = GetNextParser();
                if (next == null)
                    return 0;

                _first = false;
                _currentStream = next.GetOctetStream();
            }

            var totalRead = 0;

            for (;;)
            {
                var numRead = _currentStream.Read(buffer, offset + totalRead, count - totalRead);

                if (numRead > 0)
                {
                    totalRead += numRead;

                    if (totalRead == count)
                        return totalRead;
                }
                else
                {
                    var next = GetNextParser();
                    if (next == null)
                    {
                        _currentStream = null;
                        return totalRead;
                    }

                    _currentStream = next.GetOctetStream();
                }
            }
        }

        public override int ReadByte()
        {
            if (_currentStream == null)
            {
                if (!_first)
                    return 0;

                var next = GetNextParser();
                if (next == null)
                    return 0;

                _first = false;
                _currentStream = next.GetOctetStream();
            }

            for (;;)
            {
                var b = _currentStream.ReadByte();

                if (b >= 0)
                    return b;

                var next = GetNextParser();
                if (next == null)
                {
                    _currentStream = null;
                    return -1;
                }

                _currentStream = next.GetOctetStream();
            }
        }

        private Asn1OctetStringParser GetNextParser()
        {
            var asn1Obj = _parser.ReadObject();
            if (asn1Obj == null)
                return null;

            if (asn1Obj is Asn1OctetStringParser)
                return (Asn1OctetStringParser) asn1Obj;

            throw new IOException("unknown object encountered: " + Platform.GetTypeName(asn1Obj));
        }
    }
}