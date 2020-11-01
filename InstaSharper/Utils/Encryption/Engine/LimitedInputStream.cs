using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class LimitedInputStream
        : BaseInputStream
    {
        protected readonly Stream _in;

        internal LimitedInputStream(Stream inStream, int limit)
        {
            _in = inStream;
            Limit = limit;
        }

        internal virtual int Limit { get; }

        protected virtual void SetParentEofDetect(bool on)
        {
            if (_in is IndefiniteLengthInputStream)
            {
                ((IndefiniteLengthInputStream) _in).SetEofOn00(on);
            }
        }
    }
}