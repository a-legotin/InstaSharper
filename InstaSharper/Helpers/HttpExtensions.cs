using System;
using System.Web;

namespace InstaSharper.Helpers
{
    public static class HttpExtensions
    {
        public static Uri AddQueryParameter(this Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(uri) {Query = httpValueCollection.ToString()};
            return ub.Uri;
        }

        public static Uri AddQueryParameterIfNotEmpty(this Uri uri, string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return uri;

            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);
            var ub = new UriBuilder(uri) {Query = httpValueCollection.ToString()};
            return ub.Uri;
        }
    }
}