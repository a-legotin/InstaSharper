namespace InstaSharper.Abstractions.Serialization;

public interface ISerializer<in TT, out R>
{
    T Deserialize<T>(TT content);
    R Serialize<T>(T obj);
}