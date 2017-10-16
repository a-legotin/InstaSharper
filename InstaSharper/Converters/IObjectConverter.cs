namespace InstaSharper.Converters
{
    internal interface IObjectConverter<T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}