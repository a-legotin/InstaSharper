using System;
using System.Collections.Generic;
using InstagramAPI.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramAPI.ResponseWrappers
{
    public class InstaFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("is_direct_v2_enabled")]
        public bool IsDirectV2Enabled { get; set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public List<InstaMediaItemResponse> Items { get; set; } = new List<InstaMediaItemResponse>();
    }

    public class FeedResponseDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(InstaFeedResponse));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var feed = token.ToObject<InstaFeedResponse>();
            var items = token["feed_items"];
            if (items != null)
            {
                foreach (var item in items)
                {
                    var mediaOrAd = item["media_or_ad"];
                    if (mediaOrAd == null) continue;
                    var media = mediaOrAd.ToObject<InstaMediaItemResponse>();
                    feed.Items.Add(media);
                }
            }
            else
            {
                items = token["items"];
                feed.Items = items.ToObject<List<InstaMediaItemResponse>>();
            }

            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}