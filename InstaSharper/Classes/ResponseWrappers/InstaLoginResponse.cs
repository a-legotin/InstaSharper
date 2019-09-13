using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaLoginResponse
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("logged_in_user")] public InstaUserShortResponse User { get; set; }
    }
}