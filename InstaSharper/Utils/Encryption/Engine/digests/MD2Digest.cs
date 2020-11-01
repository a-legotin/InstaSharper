using System;

namespace InstaSharper.Utils.Encryption.Engine.digests
{
    /**
    * implementation of MD2
    * as outlined in RFC1319 by B.Kaliski from RSA Laboratories April 1992
    */
    internal class MD2Digest
        : IDigest, IMemoable
    {
        private const int DigestLength = 16;
        private const int BYTE_LENGTH = 16;


        // 256-byte random permutation constructed from the digits of PI
        private static readonly byte[] S =
        {
            41, 46, 67, 201, 162, 216, 124,
            1, 61, 54, 84, 161, 236, 240,
            6, 19, 98, 167, 5, 243, 192,
            199, 115, 140, 152, 147, 43, 217,
            188, 76, 130, 202, 30, 155, 87,
            60, 253, 212, 224, 22, 103, 66,
            111, 24, 138, 23, 229, 18, 190,
            78, 196, 214, 218, 158, 222, 73,
            160, 251, 245, 142, 187, 47, 238,
            122, 169, 104, 121, 145, 21, 178,
            7, 63, 148, 194, 16, 137, 11,
            34, 95, 33, 128, 127, 93, 154,
            90, 144, 50, 39, 53, 62, 204,
            231, 191, 247, 151, 3, 255, 25,
            48, 179, 72, 165, 181, 209, 215,
            94, 146, 42, 172, 86, 170, 198,
            79, 184, 56, 210, 150, 164, 125,
            182, 118, 252, 107, 226, 156, 116,
            4, 241, 69, 157, 112, 89, 100,
            113, 135, 32, 134, 91, 207, 101,
            230, 45, 168, 2, 27, 96, 37,
            173, 174, 176, 185, 246, 28, 70,
            97, 105, 52, 64, 126, 15, 85,
            71, 163, 35, 221, 81, 175, 58,
            195, 92, 249, 206, 186, 197, 234,
            38, 44, 83, 13, 110, 133, 40,
            132, 9, 211, 223, 205, 244, 65,
            129, 77, 82, 106, 220, 55, 200,
            108, 193, 171, 250, 36, 225, 123,
            8, 12, 189, 177, 74, 120, 136,
            149, 139, 227, 99, 232, 109, 233,
            203, 213, 254, 59, 0, 29, 57,
            242, 239, 183, 14, 102, 88, 208,
            228, 166, 119, 114, 248, 235, 117,
            75, 10, 49, 68, 80, 180, 143,
            237, 31, 26, 219, 153, 141, 51,
            159, 17, 131, 20
        };

        /* check sum */

        private readonly byte[]   C = new byte[16];

        /* M buffer */

        private readonly byte[]   M = new byte[16];

        /* X buffer */
        private readonly byte[]   X = new byte[48];
        private int COff;
        private int     mOff;
        private int     xOff;

        public MD2Digest()
        {
            Reset();
        }

        public MD2Digest(MD2Digest t)
        {
            CopyIn(t);
        }

        /**
        * return the algorithm name
        *
        * @return the algorithm name
        */
        public string AlgorithmName => "MD2";

        public int GetDigestSize() => DigestLength;

        public int GetByteLength() => BYTE_LENGTH;

        /**
        * Close the digest, producing the final digest value. The doFinal
        * call leaves the digest reset.
        *
        * @param out the array the digest is to be copied into.
        * @param outOff the offset into the out array the digest is to start at.
        */
        public int DoFinal(byte[] output, int outOff)
        {
            // add padding
            var paddingByte = (byte) (M.Length - mOff);
            for (var i = mOff; i < M.Length; i++)
            {
                M[i] = paddingByte;
            }

            //do final check sum
            ProcessChecksum(M);
            // do final block process
            ProcessBlock(M);

            ProcessBlock(C);

            Array.Copy(X, xOff, output, outOff, 16);

            Reset();

            return DigestLength;
        }

        /**
        * reset the digest back to it's initial state.
        */
        public void Reset()
        {
            xOff = 0;
            for (var i = 0; i != X.Length; i++)
            {
                X[i] = 0;
            }

            mOff = 0;
            for (var i = 0; i != M.Length; i++)
            {
                M[i] = 0;
            }

            COff = 0;
            for (var i = 0; i != C.Length; i++)
            {
                C[i] = 0;
            }
        }

        /**
        * update the message digest with a single byte.
        *
        * @param in the input byte to be entered.
        */
        public void Update(byte input)
        {
            M[mOff++] = input;

            if (mOff == 16)
            {
                ProcessChecksum(M);
                ProcessBlock(M);
                mOff = 0;
            }
        }

        /**
        * update the message digest with a block of bytes.
        *
        * @param in the byte array containing the data.
        * @param inOff the offset into the byte array where the data starts.
        * @param len the length of the data.
        */
        public void BlockUpdate(byte[] input, int inOff, int length)
        {
            //
            // fill the current word
            //
            while ((mOff != 0) && (length > 0))
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }

            //
            // process whole words.
            //
            while (length > 16)
            {
                Array.Copy(input, inOff, M, 0, 16);
                ProcessChecksum(M);
                ProcessBlock(M);
                length -= 16;
                inOff += 16;
            }

            //
            // load in the remainder.
            //
            while (length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
        }

        public IMemoable Copy() => new MD2Digest(this);

        public void Reset(IMemoable other)
        {
            var d = (MD2Digest) other;

            CopyIn(d);
        }

        private void CopyIn(MD2Digest t)
        {
            Array.Copy(t.X, 0, X, 0, t.X.Length);
            xOff = t.xOff;
            Array.Copy(t.M, 0, M, 0, t.M.Length);
            mOff = t.mOff;
            Array.Copy(t.C, 0, C, 0, t.C.Length);
            COff = t.COff;
        }

        internal void ProcessChecksum(byte[] m)
        {
            int L = C[15];
            for (var i = 0; i < 16; i++)
            {
                C[i] ^= S[(m[i] ^ L) & 0xff];
                L = C[i];
            }
        }

        internal void ProcessBlock(byte[] m)
        {
            for (var i = 0; i < 16; i++)
            {
                X[i + 16] = m[i];
                X[i + 32] = (byte) (m[i] ^ X[i]);
            }

            // encrypt block
            var t = 0;

            for (var j = 0; j < 18; j++)
            {
                for (var k = 0; k < 48; k++)
                {
                    t = X[k] ^= S[t];
                    t = t & 0xff;
                }

                t = (t + j) % 256;
            }
        }
    }
}