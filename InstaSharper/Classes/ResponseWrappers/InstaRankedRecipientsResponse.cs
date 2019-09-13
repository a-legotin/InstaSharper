using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaRankedRecipientsResponse : InstaRecipientsResponse, IInstaRecipientsResponse
    {
        [JsonProperty("ranked_recipients")] public RankedRecipientResponse[] RankedRecipients { get; set; }
    }
}