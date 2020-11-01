using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /// <summary>
    /// Implementation of Chinese SM3 digest as described at
    /// http://tools.ietf.org/html/draft-shen-sm3-hash-00
    /// and at .... ( Chinese PDF )
    /// </summary>
    /// <remarks>
    /// The specification says "process a bit stream",
    /// but this is written to process bytes in blocks of 4,
    /// meaning this will process 32-bit word groups.
    /// But so do also most other digest specifications,
    /// including the SHA-256 which was a origin for
    /// this specification.
    /// </remarks>
    internal class SM3Digest
        : GeneralDigest
    {
        private const int DIGEST_LENGTH = 32;   // bytes
        private const int BLOCK_SIZE = 64 / 4; // of 32 bit ints (16 ints)

        // Round constant T for processBlock() which is 32 bit integer rolled left up to (63 MOD 32) bit positions.
        private static readonly uint[] T = new uint[64];
        private readonly uint[] inwords = new uint[BLOCK_SIZE];

        private readonly uint[] V = new uint[DIGEST_LENGTH / 4]; // in 32 bit ints (8 ints)

        // Work-bufs used within processBlock()
        private readonly uint[] W = new uint[68];
        private int xOff;

        static SM3Digest()
        {
            for (var i = 0; i < 16; ++i)
            {
                uint t = 0x79CC4519;
                T[i] = (t << i) | (t >> (32 - i));
            }

            for (var i = 16; i < 64; ++i)
            {
                var n = i % 32;
                uint t = 0x7A879D8A;
                T[i] = (t << n) | (t >> (32 - n));
            }
        }


        /// <summary>
        /// Standard constructor
        /// </summary>
        public SM3Digest()
        {
            Reset();
        }

        /// <summary>
        /// Copy constructor.  This will copy the state of the provided
        /// message digest.
        /// </summary>
        public SM3Digest(SM3Digest t)
            : base(t)
        {
            CopyIn(t);
        }

        public override string AlgorithmName => "SM3";

        private void CopyIn(SM3Digest t)
        {
            Array.Copy(t.V, 0, V, 0, V.Length);
            Array.Copy(t.inwords, 0, inwords, 0, inwords.Length);
            xOff = t.xOff;
        }

        public override int GetDigestSize() => DIGEST_LENGTH;

        public override IMemoable Copy() => new SM3Digest(this);

        public override void Reset(IMemoable other)
        {
            var d = (SM3Digest) other;

            base.CopyIn(d);
            CopyIn(d);
        }

        /// <summary>
        /// reset the chaining variables
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            V[0] = 0x7380166F;
            V[1] = 0x4914B2B9;
            V[2] = 0x172442D7;
            V[3] = 0xDA8A0600;
            V[4] = 0xA96F30BC;
            V[5] = 0x163138AA;
            V[6] = 0xE38DEE4D;
            V[7] = 0xB0FB0E4E;

            xOff = 0;
        }


        public override int DoFinal(byte[] output, int outOff)
        {
            Finish();

            Pack.UInt32_To_BE(V, output, outOff);

            Reset();

            return DIGEST_LENGTH;
        }


        internal override void ProcessWord(byte[] input,
            int inOff)
        {
            var n = Pack.BE_To_UInt32(input, inOff);
            inwords[xOff] = n;
            ++xOff;

            if (xOff >= 16)
            {
                ProcessBlock();
            }
        }

        internal override void ProcessLength(long bitLength)
        {
            if (xOff > (BLOCK_SIZE - 2))
            {
                // xOff == 15  --> can't fit the 64 bit length field at tail..
                inwords[xOff] = 0; // fill with zero
                ++xOff;

                ProcessBlock();
            }

            // Fill with zero words, until reach 2nd to last slot
            while (xOff < (BLOCK_SIZE - 2))
            {
                inwords[xOff] = 0;
                ++xOff;
            }

            // Store input data length in BITS
            inwords[xOff++] = (uint) (bitLength >> 32);
            inwords[xOff++] = (uint) (bitLength);
        }

        /*

    3.4.2.  Constants


       Tj = 79cc4519        when 0  < = j < = 15
       Tj = 7a879d8a        when 16 < = j < = 63

    3.4.3.  Boolean function


       FFj(X;Y;Z) = X XOR Y XOR Z                       when 0  < = j < = 15
                  = (X AND Y) OR (X AND Z) OR (Y AND Z) when 16 < = j < = 63

       GGj(X;Y;Z) = X XOR Y XOR Z                       when 0  < = j < = 15
                  = (X AND Y) OR (NOT X AND Z)          when 16 < = j < = 63

       The X, Y, Z in the fomular are words!GBP

    3.4.4.  Permutation function


       P0(X) = X XOR (X <<<  9) XOR (X <<< 17)   ## ROLL, not SHIFT
       P1(X) = X XOR (X <<< 15) XOR (X <<< 23)   ## ROLL, not SHIFT

       The X in the fomular are a word.

    ----------

    Each ROLL converted to Java expression:

    ROLL 9  :  ((x <<  9) | (x >> (32-9))))
    ROLL 17 :  ((x << 17) | (x >> (32-17)))
    ROLL 15 :  ((x << 15) | (x >> (32-15)))
    ROLL 23 :  ((x << 23) | (x >> (32-23)))

     */

        private uint P0(uint x)
        {
            var r9 = ((x << 9) | (x >> (32 - 9)));
            var r17 = ((x << 17) | (x >> (32 - 17)));
            return (x ^ r9 ^ r17);
        }

        private uint P1(uint x)
        {
            var r15 = ((x << 15) | (x >> (32 - 15)));
            var r23 = ((x << 23) | (x >> (32 - 23)));
            return (x ^ r15 ^ r23);
        }

        private uint FF0(uint x, uint y, uint z) => (x ^ y ^ z);

        private uint FF1(uint x, uint y, uint z) => ((x & y) | (x & z) | (y & z));

        private uint GG0(uint x, uint y, uint z) => (x ^ y ^ z);

        private uint GG1(uint x, uint y, uint z) => ((x & y) | ((~x) & z));


        internal override void ProcessBlock()
        {
            for (var j = 0; j < 16; ++j)
            {
                W[j] = inwords[j];
            }

            for (var j = 16; j < 68; ++j)
            {
                var wj3 = W[j - 3];
                var r15 = ((wj3 << 15) | (wj3 >> (32 - 15)));
                var wj13 = W[j - 13];
                var r7 = ((wj13 << 7) | (wj13 >> (32 - 7)));
                W[j] = P1(W[j - 16] ^ W[j - 9] ^ r15) ^ r7 ^ W[j - 6];
            }

            var A = V[0];
            var B = V[1];
            var C = V[2];
            var D = V[3];
            var E = V[4];
            var F = V[5];
            var G = V[6];
            var H = V[7];


            for (var j = 0; j < 16; ++j)
            {
                var a12 = ((A << 12) | (A >> (32 - 12)));
                var s1_ = a12 + E + T[j];
                var SS1 = ((s1_ << 7) | (s1_ >> (32 - 7)));
                var SS2 = SS1 ^ a12;
                var Wj = W[j];
                var W1j = Wj ^ W[j + 4];
                var TT1 = FF0(A, B, C) + D + SS2 + W1j;
                var TT2 = GG0(E, F, G) + H + SS1 + Wj;
                D = C;
                C = ((B << 9) | (B >> (32 - 9)));
                B = A;
                A = TT1;
                H = G;
                G = ((F << 19) | (F >> (32 - 19)));
                F = E;
                E = P0(TT2);
            }

            // Different FF,GG functions on rounds 16..63
            for (var j = 16; j < 64; ++j)
            {
                var a12 = ((A << 12) | (A >> (32 - 12)));
                var s1_ = a12 + E + T[j];
                var SS1 = ((s1_ << 7) | (s1_ >> (32 - 7)));
                var SS2 = SS1 ^ a12;
                var Wj = W[j];
                var W1j = Wj ^ W[j + 4];
                var TT1 = FF1(A, B, C) + D + SS2 + W1j;
                var TT2 = GG1(E, F, G) + H + SS1 + Wj;
                D = C;
                C = ((B << 9) | (B >> (32 - 9)));
                B = A;
                A = TT1;
                H = G;
                G = ((F << 19) | (F >> (32 - 19)));
                F = E;
                E = P0(TT2);
            }

            V[0] ^= A;
            V[1] ^= B;
            V[2] ^= C;
            V[3] ^= D;
            V[4] ^= E;
            V[5] ^= F;
            V[6] ^= G;
            V[7] ^= H;

            xOff = 0;
        }
    }
}