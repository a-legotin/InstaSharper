namespace InstaSharper.Converters
{
    public interface IObjectConverter<out T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}