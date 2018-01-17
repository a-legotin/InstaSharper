using System.Collections.Generic;
using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaExploreItemsResponse : BaseLoadableResponse
    {
        [JsonIgnore] public InstaStoryTrayResponse StoryTray { get; set; } = new InstaStoryTrayResponse();

        [JsonIgnore] public List<InstaMediaItemResponse> Medias { get; set; } = new List<InstaMediaItemResponse>();

        [JsonIgnore] public InstaChannelResponse Channel { get; set; }
    }
}