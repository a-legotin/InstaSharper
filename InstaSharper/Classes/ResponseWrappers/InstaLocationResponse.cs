using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaLocationResponse 
    {
        [JsonProperty("location")] public InstaLocationShortResponse Location { get; set; }
        [JsonProperty("x")] public double X { get; set; }

        [JsonProperty("y")] public double Y { get; set; }

        [JsonProperty("z")] public double Z { get; set; }

        [JsonProperty("width")] public double Width { get; set; }

        [JsonProperty("height")] public double Height { get; set; }

        [JsonProperty("rotation")] public double Rotation { get; set; }

        [JsonProperty("is_pinned")] public long IsPinned { get; set; }

        [JsonProperty("is_hidden")] public long IsHidden { get; set; }


    }
}