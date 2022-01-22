using System.Text.Json.Serialization;
using InstaSharper.Models.Response.Base;

namespace InstaSharper.Models.Response.Story;

internal class InstaStoryPollVotersListContainerResponse : BaseStatusResponse
{
    [JsonPropertyName("voter_info")]
    public InstaStoryPollVotersListResponse VoterInfo { get; set; }
}