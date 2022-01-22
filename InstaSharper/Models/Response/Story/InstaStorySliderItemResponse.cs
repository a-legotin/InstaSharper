using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.Story;

internal class InstaStorySliderItemResponse
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }

    [JsonPropertyName("z")]
    public float Z { get; set; }

    [JsonPropertyName("width")]
    public float Width { get; set; }

    [JsonPropertyName("height")]
    public float Height { get; set; }

    [JsonPropertyName("rotation")]
    public float Rotation { get; set; }

    [JsonPropertyName("is_pinned")]
    public int IsPinned { get; set; }

    [JsonPropertyName("is_hidden")]
    public int IsHidden { get; set; }

    [JsonPropertyName("slider_sticker")]
    public InstaStorySliderStickerItemResponse SliderSticker { get; set; }
}