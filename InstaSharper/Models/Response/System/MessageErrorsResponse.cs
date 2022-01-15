using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaSharper.Models.Response.System
{
    internal class MessageErrorsResponse
    {
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}