using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.ResponseWrappers
{
    internal class InstaImageCandidatesResponse
    {
        [JsonProperty("candidates")]
        public List<ImageResponse> Candidates { get; set; }
    }
}