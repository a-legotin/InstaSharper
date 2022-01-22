using System.Net;

namespace InstaSharper.Http;

internal interface IHttpClientState
{
    CookieContainer GetCookieContainer();
    void SetCookies(CookieCollection cookies);
}