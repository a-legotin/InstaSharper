namespace InstaSharper.Utils.Encryption.Engine
{
    internal interface IGcmExponentiator
    {
        void Init(byte[] x);
        void ExponentiateX(long pow, byte[] output);
    }
}