using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /**
     * implementation of SHA-1 as outlined in "Handbook of Applied Cryptography", pages 346 - 349.
     *
     * It is interesting to ponder why the, apart from the extra IV, the other difference here from MD5
     * is the "endianness" of the word processing!
     */
    internal class Sha1Digest
        : GeneralDigest
    {
        private const int DigestLength = 20;

        //
        // Additive constants
        //
        private const uint Y1 = 0x5a827999;
        private const uint Y2 = 0x6ed9eba1;
        private const uint Y3 = 0x8f1bbcdc;
        private const uint Y4 = 0xca62c1d6;

        private readonly uint[] X = new uint[80];

        private uint H1, H2, H3, H4, H5;
        private int xOff;

        public Sha1Digest()
        {
            Reset();
        }

        /**
         * Copy constructor.  This will copy the state of the provided
         * message digest.
         */
        public Sha1Digest(Sha1Digest t)
            : base(t)
        {
            CopyIn(t);
        }

        public override string AlgorithmName => "SHA-1";

        private void CopyIn(Sha1Digest t)
        {
            base.CopyIn(t);

            H1 = t.H1;
            H2 = t.H2;
            H3 = t.H3;
            H4 = t.H4;
            H5 = t.H5;

            Array.Copy(t.X, 0, X, 0, t.X.Length);
            xOff = t.xOff;
        }

        public override int GetDigestSize() => DigestLength;

        internal override void ProcessWord(
            byte[]  input,
            int     inOff)
        {
            X[xOff] = Pack.BE_To_UInt32(input, inOff);

            if (++xOff == 16)
            {
                ProcessBlock();
            }
        }

        internal override void ProcessLength(long    bitLength)
        {
            if (xOff > 14)
            {
                ProcessBlock();
            }

            X[14] = (uint) ((ulong) bitLength >> 32);
            X[15] = (uint) ((ulong) bitLength);
        }

        public override int DoFinal(
            byte[]  output,
            int     outOff)
        {
            Finish();

            Pack.UInt32_To_BE(H1, output, outOff);
            Pack.UInt32_To_BE(H2, output, outOff + 4);
            Pack.UInt32_To_BE(H3, output, outOff + 8);
            Pack.UInt32_To_BE(H4, output, outOff + 12);
            Pack.UInt32_To_BE(H5, output, outOff + 16);

            Reset();

            return DigestLength;
        }

        /**
         * reset the chaining variables
         */
        public override void Reset()
        {
            base.Reset();

            H1 = 0x67452301;
            H2 = 0xefcdab89;
            H3 = 0x98badcfe;
            H4 = 0x10325476;
            H5 = 0xc3d2e1f0;

            xOff = 0;
            Array.Clear(X, 0, X.Length);
        }

        private static uint F(uint u, uint v, uint w) => (u & v) | (~u & w);

        private static uint H(uint u, uint v, uint w) => u ^ v ^ w;

        private static uint G(uint u, uint v, uint w) => (u & v) | (u & w) | (v & w);

        internal override void ProcessBlock()
        {
            //
            // expand 16 word block into 80 word block.
            //
            for (var i = 16; i < 80; i++)
            {
                var t = X[i - 3] ^ X[i - 8] ^ X[i - 14] ^ X[i - 16];
                X[i] = t << 1 | t >> 31;
            }

            //
            // set up working variables.
            //
            var A = H1;
            var B = H2;
            var C = H3;
            var D = H4;
            var E = H5;

            //
            // round 1
            //
            var idx = 0;

            for (var j = 0; j < 4; j++)
            {
                // E = rotateLeft(A, 5) + F(B, C, D) + E + X[idx++] + Y1
                // B = rotateLeft(B, 30)
                E += (A << 5 | (A >> 27)) + F(B, C, D) + X[idx++] + Y1;
                B = B << 30 | (B >> 2);

                D += (E << 5 | (E >> 27)) + F(A, B, C) + X[idx++] + Y1;
                A = A << 30 | (A >> 2);

                C += (D << 5 | (D >> 27)) + F(E, A, B) + X[idx++] + Y1;
                E = E << 30 | (E >> 2);

                B += (C << 5 | (C >> 27)) + F(D, E, A) + X[idx++] + Y1;
                D = D << 30 | (D >> 2);

                A += (B << 5 | (B >> 27)) + F(C, D, E) + X[idx++] + Y1;
                C = C << 30 | (C >> 2);
            }

            //
            // round 2
            //
            for (var j = 0; j < 4; j++)
            {
                // E = rotateLeft(A, 5) + H(B, C, D) + E + X[idx++] + Y2
                // B = rotateLeft(B, 30)
                E += (A << 5 | (A >> 27)) + H(B, C, D) + X[idx++] + Y2;
                B = B << 30 | (B >> 2);

                D += (E << 5 | (E >> 27)) + H(A, B, C) + X[idx++] + Y2;
                A = A << 30 | (A >> 2);

                C += (D << 5 | (D >> 27)) + H(E, A, B) + X[idx++] + Y2;
                E = E << 30 | (E >> 2);

                B += (C << 5 | (C >> 27)) + H(D, E, A) + X[idx++] + Y2;
                D = D << 30 | (D >> 2);

                A += (B << 5 | (B >> 27)) + H(C, D, E) + X[idx++] + Y2;
                C = C << 30 | (C >> 2);
            }

            //
            // round 3
            //
            for (var j = 0; j < 4; j++)
            {
                // E = rotateLeft(A, 5) + G(B, C, D) + E + X[idx++] + Y3
                // B = rotateLeft(B, 30)
                E += (A << 5 | (A >> 27)) + G(B, C, D) + X[idx++] + Y3;
                B = B << 30 | (B >> 2);

                D += (E << 5 | (E >> 27)) + G(A, B, C) + X[idx++] + Y3;
                A = A << 30 | (A >> 2);

                C += (D << 5 | (D >> 27)) + G(E, A, B) + X[idx++] + Y3;
                E = E << 30 | (E >> 2);

                B += (C << 5 | (C >> 27)) + G(D, E, A) + X[idx++] + Y3;
                D = D << 30 | (D >> 2);

                A += (B << 5 | (B >> 27)) + G(C, D, E) + X[idx++] + Y3;
                C = C << 30 | (C >> 2);
            }

            //
            // round 4
            //
            for (var j = 0; j < 4; j++)
            {
                // E = rotateLeft(A, 5) + H(B, C, D) + E + X[idx++] + Y4
                // B = rotateLeft(B, 30)
                E += (A << 5 | (A >> 27)) + H(B, C, D) + X[idx++] + Y4;
                B = B << 30 | (B >> 2);

                D += (E << 5 | (E >> 27)) + H(A, B, C) + X[idx++] + Y4;
                A = A << 30 | (A >> 2);

                C += (D << 5 | (D >> 27)) + H(E, A, B) + X[idx++] + Y4;
                E = E << 30 | (E >> 2);

                B += (C << 5 | (C >> 27)) + H(D, E, A) + X[idx++] + Y4;
                D = D << 30 | (D >> 2);

                A += (B << 5 | (B >> 27)) + H(C, D, E) + X[idx++] + Y4;
                C = C << 30 | (C >> 2);
            }

            H1 += A;
            H2 += B;
            H3 += C;
            H4 += D;
            H5 += E;

            //
            // reset start of the buffer.
            //
            xOff = 0;
            Array.Clear(X, 0, 16);
        }

        public override IMemoable Copy() => new Sha1Digest(this);

        public override void Reset(IMemoable other)
        {
            var d = (Sha1Digest) other;

            CopyIn(d);
        }
    }
}