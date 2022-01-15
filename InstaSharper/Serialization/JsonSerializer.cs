using System.Text.Json;
using InstaSharper.Abstractions.Serialization;

namespace InstaSharper.Serialization
{
    internal class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string content) => System.Text.Json.JsonSerializer.Deserialize<T>(content);
        public string Serialize<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj);
        public string SerializeIndented<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
    }
}