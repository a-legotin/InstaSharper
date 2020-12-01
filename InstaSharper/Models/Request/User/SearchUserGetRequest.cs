using InstaSharper.Utils;

namespace InstaSharper.Models.Request.User
{
    internal class SearchUserGetRequest : GetRequestBase
    {
        public static SearchUserGetRequest Build(string rankToken)
        {
            var request = new SearchUserGetRequest();
            request.Headers.Add("timezone_offset",  Constants.TIMEZONE_OFFSET.ToString());
            request.Headers.Add("count", "1");
            request.Headers.Add("rank_token", rankToken);
            return request;
        }
    }
}