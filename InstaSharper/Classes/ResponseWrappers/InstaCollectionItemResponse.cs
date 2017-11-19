using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaCollectionItemResponse : BaseLoadableResponse
    {
        [JsonProperty("collection_id")]
        public long CollectionId { get; set; }

        [JsonProperty("collection_name")]
        public string CollectionName { get; set; }

        [JsonProperty("has_related_media")]
        public bool HasRelatedMedia { get; set; }

        [JsonProperty("cover_media")]
        public string CoverMedia { get; set; }

        [JsonProperty("items")]
        public InstaMediaListResponse Media { get; set; }
    }
}
