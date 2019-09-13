﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaTopLiveResponse
    {
        [JsonProperty("ranked_position")] public int RankedPosition { get; set; }

        [JsonProperty("broadcast_owners")]
        public List<InstaUserShortResponse> BroadcastOwners { get; set; } = new List<InstaUserShortResponse>();
    }
}