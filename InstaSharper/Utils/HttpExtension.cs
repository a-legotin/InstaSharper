using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace InstaSharper.Utils;

public static class HttpExtension
{
    public static string ToQueryString(this NameValueCollection nvc)
    {
        var array = nvc.AllKeys.SelectMany(key => nvc.GetValues(key),
            (key,
             value) => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}").ToArray();
        return string.Join("&", array);
    }
}