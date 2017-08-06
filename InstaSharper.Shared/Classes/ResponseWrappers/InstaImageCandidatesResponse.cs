using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class InstaImageCandidatesResponse
    {
        [JsonProperty("candidates")]
        public List<ImageResponse> Candidates { get; set; }
    }
}