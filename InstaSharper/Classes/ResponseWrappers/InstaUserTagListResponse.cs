﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaUserTagListResponse
    {
        [JsonProperty("in")] public List<InstaUserTagResponse> In { get; set; } = new List<InstaUserTagResponse>();
    }
}