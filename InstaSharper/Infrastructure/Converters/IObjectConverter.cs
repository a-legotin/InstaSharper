namespace InstaSharper.Infrastructure.Converters;

internal interface IObjectConverter<out T, in TT>
{
    T Convert(TT source);
}