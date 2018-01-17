using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class RankedRecipientResponse
    {
        [JsonProperty("thread")] public RankedRecipientThreadResponse Thread { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }
    }
}