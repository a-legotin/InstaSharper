using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaLocationResponse
    {
        [JsonProperty("facebook_places_id")]
        public long FacebookPlacesId { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("external_source")]
        public string ExternalSource { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("pk")]
        public long Pk { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }
}