using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaDirectInboxThreadLastSeenResponse : BaseStatusResponse
    {
        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("timestamp")] public string TimeStamp { get; set; }

        [JsonProperty("item_id")] public string ItemId { get; set; }
    }
}
