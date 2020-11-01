using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /**
    * base implementation of MD4 family style digest as outlined in
    * "Handbook of Applied Cryptography", pages 344 - 347.
    */
    internal abstract class GeneralDigest
        : IDigest, IMemoable
    {
        private const int BYTE_LENGTH = 64;

        private readonly byte[]  xBuf;

        private long    byteCount;
        private int     xBufOff;

        internal GeneralDigest() => xBuf = new byte[4];

        internal GeneralDigest(GeneralDigest t)
        {
            xBuf = new byte[t.xBuf.Length];
            CopyIn(t);
        }

        public void Update(byte input)
        {
            xBuf[xBufOff++] = input;

            if (xBufOff == xBuf.Length)
            {
                ProcessWord(xBuf, 0);
                xBufOff = 0;
            }

            byteCount++;
        }

        public void BlockUpdate(
            byte[]  input,
            int     inOff,
            int     length)
        {
            length = Math.Max(0, length);

            //
            // fill the current word
            //
            var i = 0;
            if (xBufOff != 0)
            {
                while (i < length)
                {
                    xBuf[xBufOff++] = input[inOff + i++];
                    if (xBufOff == 4)
                    {
                        ProcessWord(xBuf, 0);
                        xBufOff = 0;
                        break;
                    }
                }
            }

            //
            // process whole words.
            //
            var limit = ((length - i) & ~3) + i;
            for (; i < limit; i += 4)
            {
                ProcessWord(input, inOff + i);
            }

            //
            // load in the remainder.
            //
            while (i < length)
            {
                xBuf[xBufOff++] = input[inOff + i++];
            }

            byteCount += length;
        }

        public virtual void Reset()
        {
            byteCount = 0;
            xBufOff = 0;
            Array.Clear(xBuf, 0, xBuf.Length);
        }

        public int GetByteLength() => BYTE_LENGTH;

        public abstract string AlgorithmName { get; }
        public abstract int GetDigestSize();
        public abstract int DoFinal(byte[] output, int outOff);
        public abstract IMemoable Copy();
        public abstract void Reset(IMemoable t);

        protected void CopyIn(GeneralDigest t)
        {
            Array.Copy(t.xBuf, 0, xBuf, 0, t.xBuf.Length);

            xBufOff = t.xBufOff;
            byteCount = t.byteCount;
        }

        public void Finish()
        {
            var    bitLength = (byteCount << 3);

            //
            // add the pad bytes.
            //
            Update(128);

            while (xBufOff != 0) Update(0);
            ProcessLength(bitLength);
            ProcessBlock();
        }

        internal abstract void ProcessWord(byte[] input, int inOff);
        internal abstract void ProcessLength(long bitLength);
        internal abstract void ProcessBlock();
    }
}