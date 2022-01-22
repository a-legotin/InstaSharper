using System;

namespace InstaSharper.Utils.Encryption.Engine;

internal class ParametersWithIV
    : ICipherParameters
{
    private readonly byte[] iv;

    public ParametersWithIV(ICipherParameters parameters,
                            byte[] iv)
        : this(parameters, iv, 0, iv.Length)
    {
    }

    public ParametersWithIV(ICipherParameters parameters,
                            byte[] iv,
                            int ivOff,
                            int ivLen)
    {
        // NOTE: 'parameters' may be null to imply key re-use
        if (iv == null)
            throw new ArgumentNullException("iv");

        Parameters = parameters;
        this.iv = Arrays.CopyOfRange(iv, ivOff, ivOff + ivLen);
    }

    public ICipherParameters Parameters { get; }

    public byte[] GetIV()
    {
        return (byte[])iv.Clone();
    }
}