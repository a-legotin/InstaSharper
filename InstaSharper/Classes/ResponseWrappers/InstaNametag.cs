using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaNametag
    {

        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("gradient")]
        public int Gradient { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }

        [JsonProperty("selfie_sticker")]
        public int SelfieSticker { get; set; }
    }
}