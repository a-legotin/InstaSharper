using System;
using System.Diagnostics;
using System.IO;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class BaseOutputStream : Stream
    {
        private bool closed;

        public sealed override bool CanRead => false;
        public sealed override bool CanSeek => false;
        public sealed override bool CanWrite => !closed;

#if PORTABLE
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                closed = true;
            }
            base.Dispose(disposing);
        }
#else
        public override void Close()
        {
            closed = true;
            base.Close();
        }
#endif

        public override void Flush()
        {
        }

        public sealed override long Length => throw new NotSupportedException();

        public sealed override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public sealed override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public sealed override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public sealed override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Debug.Assert(buffer != null);
            Debug.Assert(0 <= offset && offset <= buffer.Length);
            Debug.Assert(count >= 0);

            var end = offset + count;

            Debug.Assert(0 <= end && end <= buffer.Length);

            for (var i = offset; i < end; ++i)
            {
                WriteByte(buffer[i]);
            }
        }

        public virtual void Write(params byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public override void WriteByte(byte b)
        {
            Write(new[] {b}, 0, 1);
        }
    }
}