using System.Text.Json;
using InstaSharper.Abstractions.Serialization;

namespace InstaSharper.Serialization
{
    internal class JsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            Converters = { new BoolConverter() },
        };
        
        public T Deserialize<T>(string content) => System.Text.Json.JsonSerializer.Deserialize<T>(content, Options);
        public string Serialize<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj);
        public string SerializeIndented<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
    }
}