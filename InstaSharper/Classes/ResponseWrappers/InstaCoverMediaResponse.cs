using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    class InstaCoverMediaResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("image_versions2")]
        public InstaImageCandidatesResponse ImageVersions { get; set; }

        [JsonProperty("media_type")]
        public int MediaType { get; set; }

        [JsonProperty("original_height")]
        public int OriginalHeight { get; set; }

        [JsonProperty("original_width")]
        public int OriginalWidth { get; set; }
    }
}
