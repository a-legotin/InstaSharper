using System.Collections.Generic;
using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.User;

internal class InstaUserListShortResponse : BaseStatusResponse
{
    [JsonPropertyName("users")]
    public List<InstaUserShortResponse> Items { get; set; }

    [JsonPropertyName("big_list")]
    public bool IsBigList { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("next_max_id")]
    public string NextMaxId { get; set; }
}