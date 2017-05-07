using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    class InstaStoryMediaResponse
    {
        [JsonProperty("media")]
        public InstaStoryItemResponse Media { get; set; }
    }
}
