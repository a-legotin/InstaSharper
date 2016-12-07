using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaFriendshipStatusResponse
    {
        [JsonProperty("following")]
        public bool Foolowing { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("incoming_request")]
        public bool IncomingRequest { get; set; }

        [JsonProperty("outgoing_request")]
        public bool OutgoingRequest { get; set; }
    }
}