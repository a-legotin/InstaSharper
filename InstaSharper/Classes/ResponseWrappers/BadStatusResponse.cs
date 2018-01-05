﻿using InstaSharper.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class BadStatusResponse : BaseStatusResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }

        [JsonProperty("checkpoint_url")]
        public string CheckPointUrl { get; set; }
    }
}