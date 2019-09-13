using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class FbSearchPlaceResponse
    {
        [JsonProperty("items")] public IList<FbPlace> Items { get; set; }

        [JsonProperty("has_more")] public bool HasMore { get; set; }

        [JsonProperty("rank_token")] public string RankToken { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}