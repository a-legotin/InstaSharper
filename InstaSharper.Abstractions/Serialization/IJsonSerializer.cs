namespace InstaSharper.Abstractions.Serialization
{
    public interface IJsonSerializer : ISerializer<string, string>
    {
        string SerializeIndented<T>(T obj);
    }
}