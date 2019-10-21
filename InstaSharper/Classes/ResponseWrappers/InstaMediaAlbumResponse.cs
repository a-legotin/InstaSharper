using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaMediaAlbumResponse
    {
        [JsonProperty("media")] public InstaMediaItemResponse Media { get; set; }

        [JsonProperty("client_sidecar_id")] public string ClientSidecarId { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}
