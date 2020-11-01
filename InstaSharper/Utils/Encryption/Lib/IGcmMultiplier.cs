namespace InstaSharper.Utils.Encryption.Lib
{
	public interface IGcmMultiplier
	{
		void Init(byte[] H);
		void MultiplyH(byte[] x);
	}
}
