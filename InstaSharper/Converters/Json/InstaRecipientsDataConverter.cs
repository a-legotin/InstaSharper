using System;
using InstaSharper.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaSharper.Converters.Json
{
    public class InstaRecipientsDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstaRecipientsResponse);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var root = JToken.Load(reader);
            var recipients = root.ToObject<InstaRecipientsResponse>();
            recipients.Users.Clear();
            recipients.Threads.Clear();
            var items = root.SelectToken("ranked_recipients");
            foreach (var item in items)
            {
                var thread = item["thread"]?.ToObject<InstaDirectInboxThreadResponse>();
                var user = item["user"]?.ToObject<InstaUserResponse>();
                if (thread != null)
                {
                    recipients.Threads.Add(thread);
                    continue;
                }
                if (user != null)
                    recipients.Users.Add(user);
            }

            return recipients;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}