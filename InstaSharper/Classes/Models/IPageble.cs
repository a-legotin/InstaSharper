namespace InstaSharper.Classes.Models
{
    public interface IPageble
    {
        int Pages { get; set; }
        int PageSize { get; set; }
    }
}