﻿using System;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.Converters.Json
{
    public class InstaMediaListDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaMediaListResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var feed = root.ToObject<InstaMediaListResponse>();
            feed.Medias.Clear();
            feed.Stories.Clear();
            var items = root.SelectToken("items");
            var storiesTray = root.SelectToken("items[0].stories.tray");
            foreach (var item in items)
            {
                var media = item["media"]?.ToObject<InstaMediaItemResponse>();
                if (media == null) media = item.ToObject<InstaMediaItemResponse>();
                feed.Medias.Add(media);
            }
            if (storiesTray == null) return feed;
            foreach (var storyItem in storiesTray)
            {
                var story = storyItem.ToObject<InstaStoryResponse>();
                if (story == null) continue;
                feed.Stories.Add(story);
            }

            return feed;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}