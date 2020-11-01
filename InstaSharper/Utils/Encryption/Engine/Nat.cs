using System;
using System.Diagnostics;

namespace InstaSharper.Utils.Encryption.Engine
{
    internal abstract class Nat
    {
        private const ulong M = 0xFFFFFFFFUL;

        public static uint Add(int len, uint[] x, uint[] y, uint[] z)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[i] + y[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint Add33At(int len, uint x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = (ulong) z[zPos + 0] + x;
            z[zPos + 0] = (uint) c;
            c >>= 32;
            c += (ulong) z[zPos + 1] + 1;
            z[zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zPos + 2);
        }

        public static uint Add33At(int len, uint x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = (ulong) z[zOff + zPos] + x;
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            c += (ulong) z[zOff + zPos + 1] + 1;
            z[zOff + zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, zPos + 2);
        }

        public static uint Add33To(int len, uint x, uint[] z)
        {
            var c = (ulong) z[0] + x;
            z[0] = (uint) c;
            c >>= 32;
            c += (ulong) z[1] + 1;
            z[1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, 2);
        }

        public static uint Add33To(int len, uint x, uint[] z, int zOff)
        {
            var c = (ulong) z[zOff + 0] + x;
            z[zOff + 0] = (uint) c;
            c >>= 32;
            c += (ulong) z[zOff + 1] + 1;
            z[zOff + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, 2);
        }

        public static uint AddBothTo(int len, uint[] x, uint[] y, uint[] z)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[i] + y[i] + z[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddBothTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[xOff + i] + y[yOff + i] + z[zOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddDWordAt(int len, ulong x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = z[zPos + 0] + (x & M);
            z[zPos + 0] = (uint) c;
            c >>= 32;
            c += z[zPos + 1] + (x >> 32);
            z[zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zPos + 2);
        }

        public static uint AddDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = z[zOff + zPos] + (x & M);
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            c += z[zOff + zPos + 1] + (x >> 32);
            z[zOff + zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, zPos + 2);
        }

        public static uint AddDWordTo(int len, ulong x, uint[] z)
        {
            var c = z[0] + (x & M);
            z[0] = (uint) c;
            c >>= 32;
            c += z[1] + (x >> 32);
            z[1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, 2);
        }

        public static uint AddDWordTo(int len, ulong x, uint[] z, int zOff)
        {
            var c = z[zOff + 0] + (x & M);
            z[zOff + 0] = (uint) c;
            c >>= 32;
            c += z[zOff + 1] + (x >> 32);
            z[zOff + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, 2);
        }

        public static uint AddTo(int len, uint[] x, uint[] z)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[i] + z[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddTo(int len, uint[] x, int xOff, uint[] z, int zOff)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[xOff + i] + z[zOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddTo(int len, uint[] x, int xOff, uint[] z, int zOff, uint cIn)
        {
            ulong c = cIn;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[xOff + i] + z[zOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddToEachOther(int len, uint[] u, int uOff, uint[] v, int vOff)
        {
            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) u[uOff + i] + v[vOff + i];
                u[uOff + i] = (uint) c;
                v[vOff + i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static uint AddWordAt(int len, uint x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 1));
            var c = (ulong) x + z[zPos];
            z[zPos] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zPos + 1);
        }

        public static uint AddWordAt(int len, uint x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 1));
            var c = (ulong) x + z[zOff + zPos];
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, zPos + 1);
        }

        public static uint AddWordTo(int len, uint x, uint[] z)
        {
            var c = (ulong) x + z[0];
            z[0] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, 1);
        }

        public static uint AddWordTo(int len, uint x, uint[] z, int zOff)
        {
            var c = (ulong) x + z[zOff];
            z[zOff] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zOff, 1);
        }

        public static uint CAdd(int len, int mask, uint[] x, uint[] y, uint[] z)
        {
            var MASK = (uint) -(mask & 1);

            ulong c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (ulong) x[i] + (y[i] & MASK);
                z[i] = (uint) c;
                c >>= 32;
            }

            return (uint) c;
        }

        public static void CMov(int len, int mask, uint[] x, int xOff, uint[] z, int zOff)
        {
            var MASK = (uint) -(mask & 1);

            for (var i = 0; i < len; ++i)
            {
                uint z_i = z[zOff + i], diff = z_i ^ x[xOff + i];
                z_i ^= (diff & MASK);
                z[zOff + i] = z_i;
            }

            //uint half = 0x55555555U, rest = half << (-(int)MASK);

            //for (int i = 0; i < len; ++i)
            //{
            //    uint z_i = z[zOff + i], diff = z_i ^ x[xOff + i];
            //    z_i ^= (diff & half);
            //    z_i ^= (diff & rest);
            //    z[zOff + i] = z_i;
            //}
        }

        public static void CMov(int len, int mask, int[] x, int xOff, int[] z, int zOff)
        {
            mask = -(mask & 1);

            for (var i = 0; i < len; ++i)
            {
                int z_i = z[zOff + i], diff = z_i ^ x[xOff + i];
                z_i ^= (diff & mask);
                z[zOff + i] = z_i;
            }

            //int half = 0x55555555, rest = half << (-mask);

            //for (int i = 0; i < len; ++i)
            //{
            //    int z_i = z[zOff + i], diff = z_i ^ x[xOff + i];
            //    z_i ^= (diff & half);
            //    z_i ^= (diff & rest);
            //    z[zOff + i] = z_i;
            //}
        }

        public static int Compare(int len, uint[] x, uint[] y)
        {
            for (var i = len - 1; i >= 0; --i)
            {
                var x_i = x[i];
                var y_i = y[i];
                if (x_i < y_i)
                    return -1;
                if (x_i > y_i)
                    return 1;
            }

            return 0;
        }

        public static int Compare(int len, uint[] x, int xOff, uint[] y, int yOff)
        {
            for (var i = len - 1; i >= 0; --i)
            {
                var x_i = x[xOff + i];
                var y_i = y[yOff + i];
                if (x_i < y_i)
                    return -1;
                if (x_i > y_i)
                    return 1;
            }

            return 0;
        }

        public static void Copy(int len, uint[] x, uint[] z)
        {
            Array.Copy(x, 0, z, 0, len);
        }

        public static uint[] Copy(int len, uint[] x)
        {
            var z = new uint[len];
            Array.Copy(x, 0, z, 0, len);
            return z;
        }

        public static void Copy(int len, uint[] x, int xOff, uint[] z, int zOff)
        {
            Array.Copy(x, xOff, z, zOff, len);
        }

        public static ulong[] Copy64(int len, ulong[] x)
        {
            var z = new ulong[len];
            Array.Copy(x, 0, z, 0, len);
            return z;
        }

        public static void Copy64(int len, ulong[] x, ulong[] z)
        {
            Array.Copy(x, 0, z, 0, len);
        }

        public static void Copy64(int len, ulong[] x, int xOff, ulong[] z, int zOff)
        {
            Array.Copy(x, xOff, z, zOff, len);
        }

        public static uint[] Create(int len) => new uint[len];

        public static ulong[] Create64(int len) => new ulong[len];

        public static int CSub(int len, int mask, uint[] x, uint[] y, uint[] z)
        {
            long MASK = (uint) -(mask & 1);
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += x[i] - (y[i] & MASK);
                z[i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int CSub(int len, int mask, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
        {
            long MASK = (uint) -(mask & 1);
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += x[xOff + i] - (y[yOff + i] & MASK);
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int Dec(int len, uint[] z)
        {
            for (var i = 0; i < len; ++i)
            {
                if (--z[i] != uint.MaxValue)
                {
                    return 0;
                }
            }

            return -1;
        }

        public static int Dec(int len, uint[] x, uint[] z)
        {
            var i = 0;
            while (i < len)
            {
                var c = x[i] - 1;
                z[i] = c;
                ++i;
                if (c != uint.MaxValue)
                {
                    while (i < len)
                    {
                        z[i] = x[i];
                        ++i;
                    }

                    return 0;
                }
            }

            return -1;
        }

        public static int DecAt(int len, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= len);
            for (var i = zPos; i < len; ++i)
            {
                if (--z[i] != uint.MaxValue)
                {
                    return 0;
                }
            }

            return -1;
        }

        public static int DecAt(int len, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= len);
            for (var i = zPos; i < len; ++i)
            {
                if (--z[zOff + i] != uint.MaxValue)
                {
                    return 0;
                }
            }

            return -1;
        }

        public static bool Eq(int len, uint[] x, uint[] y)
        {
            for (var i = len - 1; i >= 0; --i)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static uint EqualTo(int len, uint[] x, uint y)
        {
            var d = x[0] ^ y;
            for (var i = 1; i < len; ++i)
            {
                d |= x[i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint EqualTo(int len, uint[] x, int xOff, uint y)
        {
            var d = x[xOff] ^ y;
            for (var i = 1; i < len; ++i)
            {
                d |= x[xOff + i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint EqualTo(int len, uint[] x, uint[] y)
        {
            uint d = 0;
            for (var i = 0; i < len; ++i)
            {
                d |= x[i] ^ y[i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint EqualTo(int len, uint[] x, int xOff, uint[] y, int yOff)
        {
            uint d = 0;
            for (var i = 0; i < len; ++i)
            {
                d |= x[xOff + i] ^ y[yOff + i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint EqualToZero(int len, uint[] x)
        {
            uint d = 0;
            for (var i = 0; i < len; ++i)
            {
                d |= x[i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint EqualToZero(int len, uint[] x, int xOff)
        {
            uint d = 0;
            for (var i = 0; i < len; ++i)
            {
                d |= x[xOff + i];
            }

            d = (d >> 1) | (d & 1);
            return (uint) (((int) d - 1) >> 31);
        }

        public static uint[] FromBigInteger(int bits, BigInteger x)
        {
            if (bits < 1)
                throw new ArgumentException();
            if (x.SignValue < 0 || x.BitLength > bits)
                throw new ArgumentException();

            var len = (bits + 31) >> 5;
            Debug.Assert(len > 0);
            var z = Create(len);

            // NOTE: Use a fixed number of loop iterations
            z[0] = (uint) x.IntValue;
            for (var i = 1; i < len; ++i)
            {
                x = x.ShiftRight(32);
                z[i] = (uint) x.IntValue;
            }

            return z;
        }

        public static ulong[] FromBigInteger64(int bits, BigInteger x)
        {
            if (bits < 1)
                throw new ArgumentException();
            if (x.SignValue < 0 || x.BitLength > bits)
                throw new ArgumentException();

            var len = (bits + 63) >> 6;
            Debug.Assert(len > 0);
            var z = Create64(len);

            // NOTE: Use a fixed number of loop iterations
            z[0] = (ulong) x.LongValue;
            for (var i = 1; i < len; ++i)
            {
                x = x.ShiftRight(64);
                z[i] = (ulong) x.LongValue;
            }

            return z;
        }

        public static uint GetBit(uint[] x, int bit)
        {
            if (bit == 0)
            {
                return x[0] & 1;
            }

            var w = bit >> 5;
            if (w < 0 || w >= x.Length)
            {
                return 0;
            }

            var b = bit & 31;
            return (x[w] >> b) & 1;
        }

        public static bool Gte(int len, uint[] x, uint[] y)
        {
            for (var i = len - 1; i >= 0; --i)
            {
                uint x_i = x[i], y_i = y[i];
                if (x_i < y_i)
                    return false;
                if (x_i > y_i)
                    return true;
            }

            return true;
        }

        public static uint Inc(int len, uint[] z)
        {
            for (var i = 0; i < len; ++i)
            {
                if (++z[i] != uint.MinValue)
                {
                    return 0;
                }
            }

            return 1;
        }

        public static uint Inc(int len, uint[] x, uint[] z)
        {
            var i = 0;
            while (i < len)
            {
                var c = x[i] + 1;
                z[i] = c;
                ++i;
                if (c != 0)
                {
                    while (i < len)
                    {
                        z[i] = x[i];
                        ++i;
                    }

                    return 0;
                }
            }

            return 1;
        }

        public static uint IncAt(int len, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= len);
            for (var i = zPos; i < len; ++i)
            {
                if (++z[i] != uint.MinValue)
                {
                    return 0;
                }
            }

            return 1;
        }

        public static uint IncAt(int len, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= len);
            for (var i = zPos; i < len; ++i)
            {
                if (++z[zOff + i] != uint.MinValue)
                {
                    return 0;
                }
            }

            return 1;
        }

        public static bool IsOne(int len, uint[] x)
        {
            if (x[0] != 1)
            {
                return false;
            }

            for (var i = 1; i < len; ++i)
            {
                if (x[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsZero(int len, uint[] x)
        {
            if (x[0] != 0)
            {
                return false;
            }

            for (var i = 1; i < len; ++i)
            {
                if (x[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int LessThan(int len, uint[] x, uint[] y)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) x[i] - y[i];
                c >>= 32;
            }

            Debug.Assert(c == 0L || c == -1L);
            return (int) c;
        }

        public static int LessThan(int len, uint[] x, int xOff, uint[] y, int yOff)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) x[xOff + i] - y[yOff + i];
                c >>= 32;
            }

            Debug.Assert(c == 0L || c == -1L);
            return (int) c;
        }

        public static void Mul(int len, uint[] x, uint[] y, uint[] zz)
        {
            zz[len] = MulWord(len, x[0], y, zz);

            for (var i = 1; i < len; ++i)
            {
                zz[i + len] = MulWordAddTo(len, x[i], y, 0, zz, i);
            }
        }

        public static void Mul(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
        {
            zz[zzOff + len] = MulWord(len, x[xOff], y, yOff, zz, zzOff);

            for (var i = 1; i < len; ++i)
            {
                zz[zzOff + i + len] = MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff + i);
            }
        }

        public static void Mul(uint[] x, int xOff, int xLen, uint[] y, int yOff, int yLen, uint[] zz, int zzOff)
        {
            zz[zzOff + yLen] = MulWord(yLen, x[xOff], y, yOff, zz, zzOff);

            for (var i = 1; i < xLen; ++i)
            {
                zz[zzOff + i + yLen] = MulWordAddTo(yLen, x[xOff + i], y, yOff, zz, zzOff + i);
            }
        }

        public static uint MulAddTo(int len, uint[] x, uint[] y, uint[] zz)
        {
            ulong zc = 0;
            for (var i = 0; i < len; ++i)
            {
                zc += MulWordAddTo(len, x[i], y, 0, zz, i) & M;
                zc += zz[i + len] & M;
                zz[i + len] = (uint) zc;
                zc >>= 32;
            }

            return (uint) zc;
        }

        public static uint MulAddTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
        {
            ulong zc = 0;
            for (var i = 0; i < len; ++i)
            {
                zc += MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff) & M;
                zc += zz[zzOff + len] & M;
                zz[zzOff + len] = (uint) zc;
                zc >>= 32;
                ++zzOff;
            }

            return (uint) zc;
        }

        public static uint Mul31BothAdd(int len, uint a, uint[] x, uint b, uint[] y, uint[] z, int zOff)
        {
            ulong c = 0, aVal = a, bVal = b;
            var i = 0;
            do
            {
                c += aVal * x[i] + bVal * y[i] + z[zOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            } while (++i < len);

            return (uint) c;
        }

        public static uint MulWord(int len, uint x, uint[] y, uint[] z)
        {
            ulong c = 0, xVal = x;
            var i = 0;
            do
            {
                c += xVal * y[i];
                z[i] = (uint) c;
                c >>= 32;
            } while (++i < len);

            return (uint) c;
        }

        public static uint MulWord(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
        {
            ulong c = 0, xVal = x;
            var i = 0;
            do
            {
                c += xVal * y[yOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            } while (++i < len);

            return (uint) c;
        }

        public static uint MulWordAddTo(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
        {
            ulong c = 0, xVal = x;
            var i = 0;
            do
            {
                c += xVal * y[yOff + i] + z[zOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            } while (++i < len);

            return (uint) c;
        }

        public static uint MulWordDwordAddAt(int len, uint x, ulong y, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 3));
            ulong c = 0, xVal = x;
            c += xVal * (uint) y + z[zPos + 0];
            z[zPos + 0] = (uint) c;
            c >>= 32;
            c += xVal * (y >> 32) + z[zPos + 1];
            z[zPos + 1] = (uint) c;
            c >>= 32;
            c += z[zPos + 2];
            z[zPos + 2] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : IncAt(len, z, zPos + 3);
        }

        public static uint ShiftDownBit(int len, uint[] z, uint c)
        {
            var i = len;
            while (--i >= 0)
            {
                var next = z[i];
                z[i] = (next >> 1) | (c << 31);
                c = next;
            }

            return c << 31;
        }

        public static uint ShiftDownBit(int len, uint[] z, int zOff, uint c)
        {
            var i = len;
            while (--i >= 0)
            {
                var next = z[zOff + i];
                z[zOff + i] = (next >> 1) | (c << 31);
                c = next;
            }

            return c << 31;
        }

        public static uint ShiftDownBit(int len, uint[] x, uint c, uint[] z)
        {
            var i = len;
            while (--i >= 0)
            {
                var next = x[i];
                z[i] = (next >> 1) | (c << 31);
                c = next;
            }

            return c << 31;
        }

        public static uint ShiftDownBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
        {
            var i = len;
            while (--i >= 0)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next >> 1) | (c << 31);
                c = next;
            }

            return c << 31;
        }

        public static uint ShiftDownBits(int len, uint[] z, int bits, uint c)
        {
            Debug.Assert(bits > 0 && bits < 32);
            var i = len;
            while (--i >= 0)
            {
                var next = z[i];
                z[i] = (next >> bits) | (c << -bits);
                c = next;
            }

            return c << -bits;
        }

        public static uint ShiftDownBits(int len, uint[] z, int zOff, int bits, uint c)
        {
            Debug.Assert(bits > 0 && bits < 32);
            var i = len;
            while (--i >= 0)
            {
                var next = z[zOff + i];
                z[zOff + i] = (next >> bits) | (c << -bits);
                c = next;
            }

            return c << -bits;
        }

        public static uint ShiftDownBits(int len, uint[] x, int bits, uint c, uint[] z)
        {
            Debug.Assert(bits > 0 && bits < 32);
            var i = len;
            while (--i >= 0)
            {
                var next = x[i];
                z[i] = (next >> bits) | (c << -bits);
                c = next;
            }

            return c << -bits;
        }

        public static uint ShiftDownBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
        {
            Debug.Assert(bits > 0 && bits < 32);
            var i = len;
            while (--i >= 0)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next >> bits) | (c << -bits);
                c = next;
            }

            return c << -bits;
        }

        public static uint ShiftDownWord(int len, uint[] z, uint c)
        {
            var i = len;
            while (--i >= 0)
            {
                var next = z[i];
                z[i] = c;
                c = next;
            }

            return c;
        }

        public static uint ShiftUpBit(int len, uint[] z, uint c)
        {
            for (var i = 0; i < len; ++i)
            {
                var next = z[i];
                z[i] = (next << 1) | (c >> 31);
                c = next;
            }

            return c >> 31;
        }

        public static uint ShiftUpBit(int len, uint[] z, int zOff, uint c)
        {
            for (var i = 0; i < len; ++i)
            {
                var next = z[zOff + i];
                z[zOff + i] = (next << 1) | (c >> 31);
                c = next;
            }

            return c >> 31;
        }

        public static uint ShiftUpBit(int len, uint[] x, uint c, uint[] z)
        {
            for (var i = 0; i < len; ++i)
            {
                var next = x[i];
                z[i] = (next << 1) | (c >> 31);
                c = next;
            }

            return c >> 31;
        }

        public static uint ShiftUpBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
        {
            for (var i = 0; i < len; ++i)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next << 1) | (c >> 31);
                c = next;
            }

            return c >> 31;
        }

        public static ulong ShiftUpBit64(int len, ulong[] x, int xOff, ulong c, ulong[] z, int zOff)
        {
            for (var i = 0; i < len; ++i)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next << 1) | (c >> 63);
                c = next;
            }

            return c >> 63;
        }

        public static uint ShiftUpBits(int len, uint[] z, int bits, uint c)
        {
            Debug.Assert(bits > 0 && bits < 32);
            for (var i = 0; i < len; ++i)
            {
                var next = z[i];
                z[i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static uint ShiftUpBits(int len, uint[] z, int zOff, int bits, uint c)
        {
            Debug.Assert(bits > 0 && bits < 32);
            for (var i = 0; i < len; ++i)
            {
                var next = z[zOff + i];
                z[zOff + i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static ulong ShiftUpBits64(int len, ulong[] z, int zOff, int bits, ulong c)
        {
            Debug.Assert(bits > 0 && bits < 64);
            for (var i = 0; i < len; ++i)
            {
                var next = z[zOff + i];
                z[zOff + i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static uint ShiftUpBits(int len, uint[] x, int bits, uint c, uint[] z)
        {
            Debug.Assert(bits > 0 && bits < 32);
            for (var i = 0; i < len; ++i)
            {
                var next = x[i];
                z[i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static uint ShiftUpBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
        {
            Debug.Assert(bits > 0 && bits < 32);
            for (var i = 0; i < len; ++i)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static ulong ShiftUpBits64(int len, ulong[] x, int xOff, int bits, ulong c, ulong[] z, int zOff)
        {
            Debug.Assert(bits > 0 && bits < 64);
            for (var i = 0; i < len; ++i)
            {
                var next = x[xOff + i];
                z[zOff + i] = (next << bits) | (c >> -bits);
                c = next;
            }

            return c >> -bits;
        }

        public static void Square(int len, uint[] x, uint[] zz)
        {
            var extLen = len << 1;
            uint c = 0;
            int j = len, k = extLen;
            do
            {
                ulong xVal = x[--j];
                var p = xVal * xVal;
                zz[--k] = (c << 31) | (uint) (p >> 33);
                zz[--k] = (uint) (p >> 1);
                c = (uint) p;
            } while (j > 0);

            var d = 0UL;
            var zzPos = 2;

            for (var i = 1; i < len; ++i)
            {
                d += SquareWordAddTo(x, i, zz);
                d += zz[zzPos];
                zz[zzPos++] = (uint) d;
                d >>= 32;
                d += zz[zzPos];
                zz[zzPos++] = (uint) d;
                d >>= 32;
            }

            Debug.Assert(0UL == d);

            ShiftUpBit(extLen, zz, x[0] << 31);
        }

        public static void Square(int len, uint[] x, int xOff, uint[] zz, int zzOff)
        {
            var extLen = len << 1;
            uint c = 0;
            int j = len, k = extLen;
            do
            {
                ulong xVal = x[xOff + --j];
                var p = xVal * xVal;
                zz[zzOff + --k] = (c << 31) | (uint) (p >> 33);
                zz[zzOff + --k] = (uint) (p >> 1);
                c = (uint) p;
            } while (j > 0);

            var d = 0UL;
            var zzPos = zzOff + 2;

            for (var i = 1; i < len; ++i)
            {
                d += SquareWordAddTo(x, xOff, i, zz, zzOff);
                d += zz[zzPos];
                zz[zzPos++] = (uint) d;
                d >>= 32;
                d += zz[zzPos];
                zz[zzPos++] = (uint) d;
                d >>= 32;
            }

            Debug.Assert(0UL == d);

            ShiftUpBit(extLen, zz, zzOff, x[xOff] << 31);
        }

        [Obsolete("Use 'SquareWordAddTo' instead")]
        public static uint SquareWordAdd(uint[] x, int xPos, uint[] z)
        {
            ulong c = 0, xVal = x[xPos];
            var i = 0;
            do
            {
                c += xVal * x[i] + z[xPos + i];
                z[xPos + i] = (uint) c;
                c >>= 32;
            } while (++i < xPos);

            return (uint) c;
        }

        [Obsolete("Use 'SquareWordAddTo' instead")]
        public static uint SquareWordAdd(uint[] x, int xOff, int xPos, uint[] z, int zOff)
        {
            ulong c = 0, xVal = x[xOff + xPos];
            var i = 0;
            do
            {
                c += xVal * (x[xOff + i] & M) + (z[xPos + zOff] & M);
                z[xPos + zOff] = (uint) c;
                c >>= 32;
                ++zOff;
            } while (++i < xPos);

            return (uint) c;
        }

        public static uint SquareWordAddTo(uint[] x, int xPos, uint[] z)
        {
            ulong c = 0, xVal = x[xPos];
            var i = 0;
            do
            {
                c += xVal * x[i] + z[xPos + i];
                z[xPos + i] = (uint) c;
                c >>= 32;
            } while (++i < xPos);

            return (uint) c;
        }

        public static uint SquareWordAddTo(uint[] x, int xOff, int xPos, uint[] z, int zOff)
        {
            ulong c = 0, xVal = x[xOff + xPos];
            var i = 0;
            do
            {
                c += xVal * (x[xOff + i] & M) + (z[xPos + zOff] & M);
                z[xPos + zOff] = (uint) c;
                c >>= 32;
                ++zOff;
            } while (++i < xPos);

            return (uint) c;
        }

        public static int Sub(int len, uint[] x, uint[] y, uint[] z)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) x[i] - y[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int Sub(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) x[xOff + i] - y[yOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int Sub33At(int len, uint x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = (long) z[zPos + 0] - x;
            z[zPos + 0] = (uint) c;
            c >>= 32;
            c += (long) z[zPos + 1] - 1;
            z[zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zPos + 2);
        }

        public static int Sub33At(int len, uint x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = (long) z[zOff + zPos] - x;
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            c += (long) z[zOff + zPos + 1] - 1;
            z[zOff + zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zOff, zPos + 2);
        }

        public static int Sub33From(int len, uint x, uint[] z)
        {
            var c = (long) z[0] - x;
            z[0] = (uint) c;
            c >>= 32;
            c += (long) z[1] - 1;
            z[1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, 2);
        }

        public static int Sub33From(int len, uint x, uint[] z, int zOff)
        {
            var c = (long) z[zOff + 0] - x;
            z[zOff + 0] = (uint) c;
            c >>= 32;
            c += (long) z[zOff + 1] - 1;
            z[zOff + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zOff, 2);
        }

        public static int SubBothFrom(int len, uint[] x, uint[] y, uint[] z)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) z[i] - x[i] - y[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int SubBothFrom(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) z[zOff + i] - x[xOff + i] - y[yOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int SubDWordAt(int len, ulong x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = z[zPos + 0] - (long) (x & M);
            z[zPos + 0] = (uint) c;
            c >>= 32;
            c += z[zPos + 1] - (long) (x >> 32);
            z[zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zPos + 2);
        }

        public static int SubDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 2));
            var c = z[zOff + zPos] - (long) (x & M);
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            c += z[zOff + zPos + 1] - (long) (x >> 32);
            z[zOff + zPos + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z,  zOff, zPos + 2);
        }

        public static int SubDWordFrom(int len, ulong x, uint[] z)
        {
            var c = z[0] - (long) (x & M);
            z[0] = (uint) c;
            c >>= 32;
            c += z[1] - (long) (x >> 32);
            z[1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, 2);
        }

        public static int SubDWordFrom(int len, ulong x, uint[] z, int zOff)
        {
            var c = z[zOff + 0] - (long) (x & M);
            z[zOff + 0] = (uint) c;
            c >>= 32;
            c += z[zOff + 1] - (long) (x >> 32);
            z[zOff + 1] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zOff, 2);
        }

        public static int SubFrom(int len, uint[] x, uint[] z)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) z[i] - x[i];
                z[i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int SubFrom(int len, uint[] x, int xOff, uint[] z, int zOff)
        {
            long c = 0;
            for (var i = 0; i < len; ++i)
            {
                c += (long) z[zOff + i] - x[xOff + i];
                z[zOff + i] = (uint) c;
                c >>= 32;
            }

            return (int) c;
        }

        public static int SubWordAt(int len, uint x, uint[] z, int zPos)
        {
            Debug.Assert(zPos <= (len - 1));
            var c = (long) z[zPos] - x;
            z[zPos] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zPos + 1);
        }

        public static int SubWordAt(int len, uint x, uint[] z, int zOff, int zPos)
        {
            Debug.Assert(zPos <= (len - 1));
            var c = (long) z[zOff + zPos] - x;
            z[zOff + zPos] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zOff, zPos + 1);
        }

        public static int SubWordFrom(int len, uint x, uint[] z)
        {
            var c = (long) z[0] - x;
            z[0] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, 1);
        }

        public static int SubWordFrom(int len, uint x, uint[] z, int zOff)
        {
            var c = (long) z[zOff + 0] - x;
            z[zOff + 0] = (uint) c;
            c >>= 32;
            return c == 0 ? 0 : DecAt(len, z, zOff, 1);
        }

        public static BigInteger ToBigInteger(int len, uint[] x)
        {
            var bs = new byte[len << 2];
            for (var i = 0; i < len; ++i)
            {
                var x_i = x[i];
                if (x_i != 0)
                {
                    Pack.UInt32_To_BE(x_i, bs, (len - 1 - i) << 2);
                }
            }

            return new BigInteger(1, bs);
        }

        public static void Zero(int len, uint[] z)
        {
            for (var i = 0; i < len; ++i)
            {
                z[i] = 0;
            }
        }

        public static void Zero64(int len, ulong[] z)
        {
            for (var i = 0; i < len; ++i)
            {
                z[i] = 0UL;
            }
        }
    }
}