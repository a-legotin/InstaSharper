using System;
using System.Collections.Generic;
using System.Text;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaCollectionsResponse : BaseLoadableResponse
    {
        [JsonProperty("items")]
        public List<InstaCollectionItemResponse> Items { get; set; }
    }
}
