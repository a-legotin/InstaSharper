namespace InstaSharper.Utils.Encryption.Lib
{
	public interface Asn1SetParser
		: IAsn1Convertible
	{
		IAsn1Convertible ReadObject();
	}
}
