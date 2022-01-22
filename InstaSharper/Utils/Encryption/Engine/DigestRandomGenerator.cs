namespace InstaSharper.Utils.Encryption.Engine;

/**
 * Random generation based on the digest with counter. Calling AddSeedMaterial will
 * always increase the entropy of the hash.
 * <p>
 *     Internal access to the digest is synchronized so a single one of these can be shared.
 * </p>
 */
internal class DigestRandomGenerator
    : IRandomGenerator
{
    private const long CYCLE_COUNT = 10;
    private readonly IDigest digest;
    private readonly byte[] seed;
    private readonly byte[] state;
    private long seedCounter;

    private long stateCounter;

    public DigestRandomGenerator(
        IDigest digest)
    {
        this.digest = digest;

        seed = new byte[digest.GetDigestSize()];
        seedCounter = 1;

        state = new byte[digest.GetDigestSize()];
        stateCounter = 1;
    }

    public void AddSeedMaterial(
        byte[] inSeed)
    {
        lock (this)
        {
            DigestUpdate(inSeed);
            DigestUpdate(seed);
            DigestDoFinal(seed);
        }
    }

    public void AddSeedMaterial(
        long rSeed)
    {
        lock (this)
        {
            DigestAddCounter(rSeed);
            DigestUpdate(seed);
            DigestDoFinal(seed);
        }
    }

    public void NextBytes(
        byte[] bytes)
    {
        NextBytes(bytes, 0, bytes.Length);
    }

    public void NextBytes(
        byte[] bytes,
        int start,
        int len)
    {
        lock (this)
        {
            var stateOff = 0;

            GenerateState();

            var end = start + len;
            for (var i = start; i < end; ++i)
            {
                if (stateOff == state.Length)
                {
                    GenerateState();
                    stateOff = 0;
                }

                bytes[i] = state[stateOff++];
            }
        }
    }

    private void CycleSeed()
    {
        DigestUpdate(seed);
        DigestAddCounter(seedCounter++);
        DigestDoFinal(seed);
    }

    private void GenerateState()
    {
        DigestAddCounter(stateCounter++);
        DigestUpdate(state);
        DigestUpdate(seed);
        DigestDoFinal(state);

        if (stateCounter % CYCLE_COUNT == 0) CycleSeed();
    }

    private void DigestAddCounter(long seedVal)
    {
        var bytes = new byte[8];
        Pack.UInt64_To_LE((ulong)seedVal, bytes);
        digest.BlockUpdate(bytes, 0, bytes.Length);
    }

    private void DigestUpdate(byte[] inSeed)
    {
        digest.BlockUpdate(inSeed, 0, inSeed.Length);
    }

    private void DigestDoFinal(byte[] result)
    {
        digest.DoFinal(result, 0);
    }
}