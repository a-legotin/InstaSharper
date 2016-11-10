using System;
using System.Net.Http;
using InstagramAPI.API;

namespace InstagramAPI.Helpers
{
    public class HttpHelper
    {
        public static HttpRequestMessage GetDefaultRequest(HttpMethod method, Uri uri)
        {
            var request = new HttpRequestMessage(method, uri);
            request.Headers.Add(InstaApiConstants.HEADER_ACCEPT_LANGUAGE,
                InstaApiConstants.ACCEPT_LANGUAGE);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CAPABILITIES,
                InstaApiConstants.IG_CAPABILITIES);
            request.Headers.Add(InstaApiConstants.HEADER_IG_CONNECTION_TYPE,
                InstaApiConstants.IG_CONNECTION_TYPE);
            request.Headers.Add(InstaApiConstants.HEADER_USER_AGENT, InstaApiConstants.USER_AGENT);
            return request;
        }
    }
}