namespace InstaSharper.Utils.Encryption.Engine.digests;

/// <summary>
///     Implementation of the Skein parameterised hash function in 256, 512 and 1024 bit block sizes,
///     based on the <see cref="ThreefishEngine">Threefish</see> tweakable block cipher.
/// </summary>
/// <remarks>
///     This is the 1.3 version of Skein defined in the Skein hash function submission to the NIST SHA-3
///     competition in October 2010.
///     <p />
///     Skein was designed by Niels Ferguson - Stefan Lucks - Bruce Schneier - Doug Whiting - Mihir
///     Bellare - Tadayoshi Kohno - Jon Callas - Jesse Walker.
/// </remarks>
/// <seealso cref="SkeinEngine" />
/// <seealso cref="SkeinParameters" />
internal class SkeinDigest
    : IDigest, IMemoable
{
    /// <summary>
    ///     256 bit block size - Skein-256
    /// </summary>
    public const int SKEIN_256 = SkeinEngine.SKEIN_256;

    /// <summary>
    ///     512 bit block size - Skein-512
    /// </summary>
    public const int SKEIN_512 = SkeinEngine.SKEIN_512;

    /// <summary>
    ///     1024 bit block size - Skein-1024
    /// </summary>
    public const int SKEIN_1024 = SkeinEngine.SKEIN_1024;

    private readonly SkeinEngine engine;

    /// <summary>
    ///     Constructs a Skein digest with an internal state size and output size.
    /// </summary>
    /// <param name="stateSizeBits">
    ///     the internal state size in bits - one of <see cref="SKEIN_256" /> <see cref="SKEIN_512" /> or
    ///     <see cref="SKEIN_1024" />.
    /// </param>
    /// <param name="digestSizeBits">
    ///     the output/digest size to produce in bits, which must be an integral number of
    ///     bytes.
    /// </param>
    public SkeinDigest(int stateSizeBits,
                       int digestSizeBits)
    {
        engine = new SkeinEngine(stateSizeBits, digestSizeBits);
        Init(null);
    }

    public SkeinDigest(SkeinDigest digest)
    {
        engine = new SkeinEngine(digest.engine);
    }

    public string AlgorithmName => "Skein-" + engine.BlockSize * 8 + "-" + engine.OutputSize * 8;

    public int GetDigestSize()
    {
        return engine.OutputSize;
    }

    public int GetByteLength()
    {
        return engine.BlockSize;
    }

    public void Reset()
    {
        engine.Reset();
    }

    public void Update(byte inByte)
    {
        engine.Update(inByte);
    }

    public void BlockUpdate(byte[] inBytes,
                            int inOff,
                            int len)
    {
        engine.Update(inBytes, inOff, len);
    }

    public int DoFinal(byte[] outBytes,
                       int outOff)
    {
        return engine.DoFinal(outBytes, outOff);
    }

    public void Reset(IMemoable other)
    {
        var d = (SkeinDigest)other;
        engine.Reset(d.engine);
    }

    public IMemoable Copy()
    {
        return new SkeinDigest(this);
    }

    /// <summary>
    ///     Optionally initialises the Skein digest with the provided parameters.
    /// </summary>
    /// See
    /// <see cref="SkeinParameters"></see>
    /// for details on the parameterisation of the Skein hash function.
    /// <param name="parameters">the parameters to apply to this engine, or <code>null</code> to use no parameters.</param>
    public void Init(SkeinParameters parameters)
    {
        engine.Init(parameters);
    }
}