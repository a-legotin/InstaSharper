﻿using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")] public int Count { get; set; }
    }
}