﻿using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaHashtagSearchResponse : BaseStatusResponse
    {
        [JsonProperty("results")] public List<InstaHashtagResponse> Tags { get; set; }

        [JsonProperty("has_more")] public bool? MoreAvailable { get; set; }

        [JsonProperty("rank_token")] public string RankToken { get; set; }
    }
}