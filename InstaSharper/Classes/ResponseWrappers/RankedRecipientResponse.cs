using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class RankedRecipientResponse
    {
        [JsonProperty("thread")]
        public RankedRecipientThreadResponse Thread { get; set; }
    }
}