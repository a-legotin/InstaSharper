using System.Net.Http;
using InstagramApi.Classes;
using InstagramApi.Logger;

namespace InstagramApi.API.Builder
{
    public interface IInstaApiBuilder
    {
        IInstaApi Build();
        IInstaApiBuilder UseLogger(ILogger logger);
        IInstaApiBuilder UseHttpClient(HttpClient httpClient);
        IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler);
        IInstaApiBuilder SetUserName(string username);
        IInstaApiBuilder SetUser(UserCredentials user);
    }
}