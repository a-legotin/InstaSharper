using InstaSharper.Abstractions.Serialization;
using Newtonsoft.Json;

namespace InstaSharper.Serialization
{
    internal class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
        public string SerializeIndented<T>(T obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}