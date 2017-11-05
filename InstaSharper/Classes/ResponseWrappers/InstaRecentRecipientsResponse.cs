using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaRecentRecipientsResponse : InstaRecipientsResponse, IInstaRecipientsResponse
    {
        [JsonProperty("recent_recipients")]
        public RankedRecipientResponse[] RankedRecipients { get; set; }
    }
}