namespace InstaSharper.Classes
{
    public interface IResult<out T>
    {
        bool Succeeded { get; }
        T Value { get; }
        ResultInfo Info { get; }
    }
}