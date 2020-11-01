using System.IO;

namespace InstaSharper.Utils.Encryption.Lib
{
	public interface Asn1OctetStringParser
		: IAsn1Convertible
	{
		Stream GetOctetStream();
	}
}
