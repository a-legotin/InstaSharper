using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaImageCandidatesResponse
    {
        [JsonProperty("candidates")] public List<ImageResponse> Candidates { get; set; }
    }
}