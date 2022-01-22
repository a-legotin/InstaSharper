namespace InstaSharper.Abstractions.Models.Story;

public class InstaStoryLocationUpload
{
    public double X { get; set; } = 0.5;
    public double Y { get; set; } = 0.5;
    public double Z { get; set; } = 0;

    public double Width { get; set; } = 0.7416667;
    public double Height { get; set; } = 0.08751394;
    public double Rotation { get; set; } = 0.0;

    /// <summary>
    ///     Location id (get it from <seealso cref="ILocationProcessor.SearchLocationAsync" /> )
    /// </summary>
    public string LocationId { get; set; }

    public bool IsSticker { get; set; } = false;
}