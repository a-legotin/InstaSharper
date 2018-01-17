using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaUserListResponse : BaseStatusResponse
    {
        [JsonProperty("users")] public List<InstaUserResponse> Items { get; set; }

        [JsonProperty("big_list")] public bool IsBigList { get; set; }

        [JsonProperty("page_size")] public int PageSize { get; set; }

        [JsonProperty("next_max_id")] public string NextMaxId { get; set; }
    }
}