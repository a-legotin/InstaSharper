using InstaSharper.Models.Response.Base;
using Newtonsoft.Json;

namespace InstaSharper.Models.Response.User
{
    internal class InstaLoginResponse : BaseStatusResponse
    {
        [JsonProperty("logged_in_user")]
        public InstaUserShortResponse User { get; set; }
    }
}