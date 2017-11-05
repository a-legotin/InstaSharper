using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class RankedRecipientResponse
    {
        [JsonProperty("thread")]
        public RankedRecipientThreadResponse Thread { get; set; }
    }
}