using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Models.Response
{
    internal class MessageErrorsResponse
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}