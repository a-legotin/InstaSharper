using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class MessageErrorsResponse
    {
        [JsonProperty("errors")] public List<string> Errors { get; set; }
    }
}