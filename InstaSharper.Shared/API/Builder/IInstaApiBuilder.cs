using System.Net.Http;
using InstaSharper.Classes;
using InstaSharper.Classes.Android.DeviceInfo;
using InstaSharper.Logger;

namespace InstaSharper.API.Builder
{
    public interface IInstaApiBuilder
    {
        IInstaApi Build();
        IInstaApiBuilder UseLogger(ILogger logger);
        IInstaApiBuilder UseHttpClient(HttpClient httpClient);
        IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler);
        IInstaApiBuilder SetUserName(string username);
        IInstaApiBuilder SetUser(UserSessionData user);
        IInstaApiBuilder SetApiRequestMessage(ApiRequestMessage requestMessage);
    }
}