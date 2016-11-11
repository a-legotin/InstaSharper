namespace InstagramAPI.Classes
{
    public interface IResult<out T>
    {
        bool Succeeded { get; }
        string Message { get; }
        T Value { get; }
    }
}