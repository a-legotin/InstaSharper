using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /**
	* implementation of GOST R 34.11-94
	*/
    internal class Gost3411Digest
        : IDigest, IMemoable
    {
        private const int DIGEST_LENGTH = 32;

        /**
		* reset the chaining variables to the IV values.
		*/
        private static readonly byte[] C2 =
        {
            0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF,
            0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00,
            0x00, 0xFF, 0xFF, 0x00, 0xFF, 0x00, 0x00, 0xFF,
            0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0xFF
        };

        //A (x) = (x0 ^ x1) || x3 || x2 || x1
        readonly byte[] a = new byte[8];
        private readonly byte[][] C = MakeC();

        private readonly IBlockCipher cipher = new Gost28147Engine();

        private readonly byte[] H = new byte[32];

        // (i + 1 + 4(k - 1)) = 8i + k      i = 0-3, k = 1-8
        private readonly byte[] K = new byte[32];

        private readonly byte[] L = new byte[32];

        private readonly byte[] M = new byte[32];
        private readonly byte[] Sum = new byte[32];

        private readonly byte[] xBuf = new byte[32];
        private ulong byteCount;

        // block processing
        internal byte[] S = new byte[32], U = new byte[32], V = new byte[32], W = new byte[32];
        private byte[] sBox;

        // (in:) n16||..||n1 ==> (out:) n1^n2^n3^n4^n13^n16||n16||..||n2
        internal short[] wS = new short[16], w_S = new short[16];
        private int xBufOff;

        /**
		 * Standard constructor
		 */
        public Gost3411Digest()
        {
            sBox = Gost28147Engine.GetSBox("D-A");
            cipher.Init(true, new ParametersWithSBox(null, sBox));

            Reset();
        }

        /**
		 * Constructor to allow use of a particular sbox with GOST28147
		 * @see GOST28147Engine#getSBox(String)
		 */
        public Gost3411Digest(byte[] sBoxParam)
        {
            sBox = Arrays.Clone(sBoxParam);
            cipher.Init(true, new ParametersWithSBox(null, sBox));

            Reset();
        }

        /**
		 * Copy constructor.  This will copy the state of the provided
		 * message digest.
		 */
        public Gost3411Digest(Gost3411Digest t)
        {
            Reset(t);
        }

        public string AlgorithmName => "Gost3411";

        public int GetDigestSize() => DIGEST_LENGTH;

        public void Update(
            byte input)
        {
            xBuf[xBufOff++] = input;
            if (xBufOff == xBuf.Length)
            {
                sumByteArray(xBuf); // calc sum M
                processBlock(xBuf, 0);
                xBufOff = 0;
            }

            byteCount++;
        }

        public void BlockUpdate(
            byte[] input,
            int inOff,
            int length)
        {
            while ((xBufOff != 0) && (length > 0))
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }

            while (length > xBuf.Length)
            {
                Array.Copy(input, inOff, xBuf, 0, xBuf.Length);

                sumByteArray(xBuf); // calc sum M
                processBlock(xBuf, 0);
                inOff += xBuf.Length;
                length -= xBuf.Length;
                byteCount += (uint) xBuf.Length;
            }

            // load in the remainder.
            while (length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
        }

        public int DoFinal(
            byte[]  output,
            int     outOff)
        {
            finish();

            H.CopyTo(output, outOff);

            Reset();

            return DIGEST_LENGTH;
        }

        public void Reset()
        {
            byteCount = 0;
            xBufOff = 0;

            Array.Clear(H, 0, H.Length);
            Array.Clear(L, 0, L.Length);
            Array.Clear(M, 0, M.Length);
            Array.Clear(C[1], 0, C[1].Length); // real index C = +1 because index array with 0.
            Array.Clear(C[3], 0, C[3].Length);
            Array.Clear(Sum, 0, Sum.Length);
            Array.Clear(xBuf, 0, xBuf.Length);

            C2.CopyTo(C[2], 0);
        }

        public int GetByteLength() => 32;

        public IMemoable Copy() => new Gost3411Digest(this);

        public void Reset(IMemoable other)
        {
            var t = (Gost3411Digest) other;

            sBox = t.sBox;
            cipher.Init(true, new ParametersWithSBox(null, sBox));

            Reset();

            Array.Copy(t.H, 0, H, 0, t.H.Length);
            Array.Copy(t.L, 0, L, 0, t.L.Length);
            Array.Copy(t.M, 0, M, 0, t.M.Length);
            Array.Copy(t.Sum, 0, Sum, 0, t.Sum.Length);
            Array.Copy(t.C[1], 0, C[1], 0, t.C[1].Length);
            Array.Copy(t.C[2], 0, C[2], 0, t.C[2].Length);
            Array.Copy(t.C[3], 0, C[3], 0, t.C[3].Length);
            Array.Copy(t.xBuf, 0, xBuf, 0, t.xBuf.Length);

            xBufOff = t.xBufOff;
            byteCount = t.byteCount;
        }

        private static byte[][] MakeC()
        {
            var c = new byte[4][];
            for (var i = 0; i < 4; ++i)
            {
                c[i] = new byte[32];
            }

            return c;
        }

        private byte[] P(byte[] input)
        {
            var fourK = 0;
            for (var k = 0; k < 8; k++)
            {
                K[fourK++] = input[k];
                K[fourK++] = input[8 + k];
                K[fourK++] = input[16 + k];
                K[fourK++] = input[24 + k];
            }

            return K;
        }

        private byte[] A(byte[] input)
        {
            for (var j = 0; j < 8; j++)
            {
                a[j] = (byte) (input[j] ^ input[j + 8]);
            }

            Array.Copy(input, 8, input, 0, 24);
            Array.Copy(a, 0, input, 24, 8);

            return input;
        }

        //Encrypt function, ECB mode
        private void E(byte[] key, byte[] s, int sOff, byte[] input, int inOff)
        {
            cipher.Init(true, new KeyParameter(key));

            cipher.ProcessBlock(input, inOff, s, sOff);
        }

        private void fw(byte[] input)
        {
            cpyBytesToShort(input, wS);
            w_S[15] = (short) (wS[0] ^ wS[1] ^ wS[2] ^ wS[3] ^ wS[12] ^ wS[15]);
            Array.Copy(wS, 1, w_S, 0, 15);
            cpyShortToBytes(w_S, input);
        }

        private void processBlock(byte[] input, int inOff)
        {
            Array.Copy(input, inOff, M, 0, 32);

            //key step 1

            // H = h3 || h2 || h1 || h0
            // S = s3 || s2 || s1 || s0
            H.CopyTo(U, 0);
            M.CopyTo(V, 0);
            for (var j = 0; j < 32; j++)
            {
                W[j] = (byte) (U[j] ^ V[j]);
            }

            // Encrypt gost28147-ECB
            E(P(W), S, 0, H, 0); // s0 = EK0 [h0]

            //keys step 2,3,4
            for (var i = 1; i < 4; i++)
            {
                var tmpA = A(U);
                for (var j = 0; j < 32; j++)
                {
                    U[j] = (byte) (tmpA[j] ^ C[i][j]);
                }

                V = A(A(V));
                for (var j = 0; j < 32; j++)
                {
                    W[j] = (byte) (U[j] ^ V[j]);
                }

                // Encrypt gost28147-ECB
                E(P(W), S, i * 8, H, i * 8); // si = EKi [hi]
            }

            // x(M, H) = y61(H^y(M^y12(S)))
            for (var n = 0; n < 12; n++)
            {
                fw(S);
            }

            for (var n = 0; n < 32; n++)
            {
                S[n] = (byte) (S[n] ^ M[n]);
            }

            fw(S);

            for (var n = 0; n < 32; n++)
            {
                S[n] = (byte) (H[n] ^ S[n]);
            }

            for (var n = 0; n < 61; n++)
            {
                fw(S);
            }

            Array.Copy(S, 0, H, 0, H.Length);
        }

        private void finish()
        {
            var bitCount = byteCount * 8;
            Pack.UInt64_To_LE(bitCount, L);

            while (xBufOff != 0)
            {
                Update(0);
            }

            processBlock(L, 0);
            processBlock(Sum, 0);
        }

        //  256 bitsblock modul -> (Sum + a mod (2^256))
        private void sumByteArray(
            byte[] input)
        {
            var carry = 0;

            for (var i = 0; i != Sum.Length; i++)
            {
                var sum = (Sum[i] & 0xff) + (input[i] & 0xff) + carry;

                Sum[i] = (byte) sum;

                carry = sum >> 8;
            }
        }

        private static void cpyBytesToShort(byte[] S, short[] wS)
        {
            for (var i = 0; i < S.Length / 2; i++)
            {
                wS[i] = (short) (((S[i * 2 + 1] << 8) & 0xFF00) | (S[i * 2] & 0xFF));
            }
        }

        private static void cpyShortToBytes(short[] wS, byte[] S)
        {
            for (var i = 0; i < S.Length / 2; i++)
            {
                S[i * 2 + 1] = (byte) (wS[i] >> 8);
                S[i * 2] = (byte) wS[i];
            }
        }
    }
}