namespace InstaSharper.Utils.Encryption.Engine;

internal interface IGcmMultiplier
{
    void Init(byte[] H);
    void MultiplyH(byte[] x);
}