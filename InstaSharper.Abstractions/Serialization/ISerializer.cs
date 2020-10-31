namespace InstaSharper.Abstractions.Serialization
{
    public interface ISerializer
    {
        T Deserialize<T>(string content);
        string Serialize<T>(T obj);
        string SerializeIndented<T>(T obj);
    }
}