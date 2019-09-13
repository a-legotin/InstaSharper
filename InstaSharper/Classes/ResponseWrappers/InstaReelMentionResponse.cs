﻿using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaReelMentionResponse
    {
        [JsonProperty("rotation")] public double Rotation { get; set; }

        [JsonProperty("height")] public double Height { get; set; }

        [JsonProperty("hashtag")] public InstaHashtagResponse Hashtag { get; set; }

        [JsonProperty("is_pinned")] public int IsPinned { get; set; }

        [JsonProperty("width")] public double Width { get; set; }

        [JsonProperty("user")] public InstaUserShortResponse User { get; set; }

        [JsonProperty("x")] public double X { get; set; }

        [JsonProperty("y")] public double Y { get; set; }
    }
}