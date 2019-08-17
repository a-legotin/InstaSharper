using System;
using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.Converters.Json
{
    internal class InstaInboxThreadLastSeenConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var Result = new List<InstaDirectInboxThreadLastSeenResponse>();

            var LastSeenJasonObject = JObject.Load(reader);

            foreach (var JObject in LastSeenJasonObject)
            {
                var ThreadLastSeen =
                    JsonConvert.DeserializeObject<InstaDirectInboxThreadLastSeenResponse>(JObject.Value.ToString());

                ThreadLastSeen.UserId = Convert.ToInt64(JObject.Key);
    
                Result.Add(ThreadLastSeen);
            }

            return Result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaDirectInboxThreadLastSeenResponse);
        }
    }
}
