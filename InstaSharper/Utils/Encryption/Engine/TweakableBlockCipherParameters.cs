namespace InstaSharper.Utils.Encryption.Engine
{
    /// <summary>
    /// Parameters for tweakable block ciphers.
    /// </summary>
    internal class TweakableBlockCipherParameters
        : ICipherParameters
    {
        public TweakableBlockCipherParameters(KeyParameter key, byte[] tweak)
        {
            Key = key;
            Tweak = Arrays.Clone(tweak);
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>the key to use, or <code>null</code> to use the current key.</value>
        public KeyParameter Key { get; }

        /// <summary>
        /// Gets the tweak value.
        /// </summary>
        /// <value>The tweak to use, or <code>null</code> to use the current tweak.</value>
        public byte[] Tweak { get; }
    }
}