namespace InstaSharper.Utils.Encryption.Engine
{
    internal class ParametersWithSBox : ICipherParameters
    {
        private readonly byte[] sBox;

        public ParametersWithSBox(
            ICipherParameters parameters,
            byte[] sBox)
        {
            Parameters = parameters;
            this.sBox = sBox;
        }

        public ICipherParameters Parameters { get; }

        public byte[] GetSBox() => sBox;
    }
}