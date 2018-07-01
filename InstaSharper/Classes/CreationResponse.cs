using Newtonsoft.Json;

namespace InstaSharper.Classes
{
    public class CreationResponse
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("account_created")] public bool AccountCreated { get; set; }
    }
}