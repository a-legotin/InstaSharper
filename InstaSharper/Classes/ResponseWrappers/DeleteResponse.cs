using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    internal class DeleteResponse : BaseStatusResponse
    {
        [JsonProperty("did_delete")]
        public bool IsDeleted { get; set; }
    }
}