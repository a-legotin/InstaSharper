using System.Net.Http;

namespace InstaSharper.Http
{
    internal interface IInstaHttpClientFactory
    {
        IInstaHttpClient Construct(HttpClient client);
    }
}