using Newtonsoft.Json;

namespace InstaSharper.Models.Response.User
{
    internal class InstaFriendshipShortStatusResponse
    {
        [JsonIgnore]
        public long Pk { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("incoming_request")]
        public bool IncomingRequest { get; set; }

        [JsonProperty("outgoing_request")]
        public bool OutgoingRequest { get; set; }

        [JsonProperty("is_bestie")]
        public bool IsBestie { get; set; }
    }
}