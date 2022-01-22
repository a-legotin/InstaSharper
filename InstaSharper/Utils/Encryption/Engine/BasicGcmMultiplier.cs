namespace InstaSharper.Utils.Encryption.Engine;

internal class BasicGcmMultiplier
    : IGcmMultiplier
{
    private uint[] H;

    public void Init(byte[] H)
    {
        this.H = GcmUtilities.AsUints(H);
    }

    public void MultiplyH(byte[] x)
    {
        var t = GcmUtilities.AsUints(x);
        GcmUtilities.Multiply(t, H);
        GcmUtilities.AsBytes(t, x);
    }
}