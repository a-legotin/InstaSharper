﻿using InstaSharper.Classes.Models;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaInboxMediaResponse
    {
        [JsonProperty("image_versions2")] public InstaImageCandidatesResponse ImageCandidates { get; set; }

        [JsonProperty("original_width")] public long OriginalWidth { get; set; }

        [JsonProperty("original_height")] public long OriginalHeight { get; set; }

        [JsonProperty("media_type")] public InstaMediaType MediaType { get; set; }
    }
}