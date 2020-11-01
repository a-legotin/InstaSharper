using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /**
     * Wrapper removes exposure to the IMemoable interface on an IDigest implementation.
     */
    internal class NonMemoableDigest
        :   IDigest
    {
        protected readonly IDigest mBaseDigest;

        /**
         * Base constructor.
         *
         * @param baseDigest underlying digest to use.
         * @exception IllegalArgumentException if baseDigest is null
         */
        public NonMemoableDigest(IDigest baseDigest)
        {
            if (baseDigest == null)
                throw new ArgumentNullException("baseDigest");

            mBaseDigest = baseDigest;
        }

        public virtual string AlgorithmName => mBaseDigest.AlgorithmName;

        public virtual int GetDigestSize() => mBaseDigest.GetDigestSize();

        public virtual void Update(byte input)
        {
            mBaseDigest.Update(input);
        }

        public virtual void BlockUpdate(byte[] input, int inOff, int len)
        {
            mBaseDigest.BlockUpdate(input, inOff, len);
        }

        public virtual int DoFinal(byte[] output, int outOff) => mBaseDigest.DoFinal(output, outOff);

        public virtual void Reset()
        {
            mBaseDigest.Reset();
        }

        public virtual int GetByteLength() => mBaseDigest.GetByteLength();
    }
}