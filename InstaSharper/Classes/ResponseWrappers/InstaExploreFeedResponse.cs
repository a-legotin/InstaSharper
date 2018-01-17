using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaExploreFeedResponse : BaseLoadableResponse
    {
        [JsonIgnore] public InstaExploreItemsResponse Items { get; set; } = new InstaExploreItemsResponse();
    }
}