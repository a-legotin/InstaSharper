using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.ResponseWrappers
{
    class DeleteResponse : BaseResponse.BaseStatusResponse
    {
        [JsonProperty("did_delete")]
        public bool IsDeleted { get; set; }
    }
}
