using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaResponse
    {
        public bool IsFirstResponse { get; set; }
        public string Status { get; set; }

        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }

        public List<InstaResponseItem> Items { get; set; }

        public string GetLastId()
        {
            var id = Items.OrderByDescending(post => post.CreatedTimeConverted).LastOrDefault()?.Id;
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            return id;
        }
    }
}