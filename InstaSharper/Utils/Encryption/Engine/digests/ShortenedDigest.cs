using System;

namespace InstaSharper.Utils.Encryption.Engine.digests;

/**
	* Wrapper class that reduces the output length of a particular digest to
	* only the first n bytes of the digest function.
	*/
internal class ShortenedDigest
    : IDigest
{
    private readonly IDigest baseDigest;
    private readonly int length;

    /**
		* Base constructor.
		*
		* @param baseDigest underlying digest to use.
		* @param length length in bytes of the output of doFinal.
		* @exception ArgumentException if baseDigest is null, or length is greater than baseDigest.GetDigestSize().
		*/
    public ShortenedDigest(
        IDigest baseDigest,
        int length)
    {
        if (baseDigest == null) throw new ArgumentNullException("baseDigest");

        if (length > baseDigest.GetDigestSize())
            throw new ArgumentException("baseDigest output not large enough to support length");

        this.baseDigest = baseDigest;
        this.length = length;
    }

    public string AlgorithmName => baseDigest.AlgorithmName + "(" + length * 8 + ")";

    public int GetDigestSize()
    {
        return length;
    }

    public void Update(byte input)
    {
        baseDigest.Update(input);
    }

    public void BlockUpdate(byte[] input,
                            int inOff,
                            int length)
    {
        baseDigest.BlockUpdate(input, inOff, length);
    }

    public int DoFinal(byte[] output,
                       int outOff)
    {
        var tmp = new byte[baseDigest.GetDigestSize()];

        baseDigest.DoFinal(tmp, 0);

        Array.Copy(tmp, 0, output, outOff, length);

        return length;
    }

    public void Reset()
    {
        baseDigest.Reset();
    }

    public int GetByteLength()
    {
        return baseDigest.GetByteLength();
    }
}