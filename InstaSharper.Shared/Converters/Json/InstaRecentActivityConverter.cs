﻿using System;
using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.Converters.Json
{
    public class InstaRecentActivityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaRecentActivityResponse);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var recentActivity = new InstaRecentActivityResponse();
            if (token.SelectToken("stories") != null)
            {
                recentActivity = token.ToObject<InstaRecentActivityResponse>();
                recentActivity.IsOwnActivity = false;
            }
            else
            {
                var oldStories = token.SelectToken("old_stories")?.ToObject<List<InstaRecentActivityFeedResponse>>();
                recentActivity.Stories.AddRange(oldStories);
                recentActivity.IsOwnActivity = true;
            }
            return recentActivity;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}