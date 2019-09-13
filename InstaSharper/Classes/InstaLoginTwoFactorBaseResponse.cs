﻿using Newtonsoft.Json;

namespace InstaSharper.Classes
{
    internal class InstaLoginTwoFactorBaseResponse
    {
        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("error_type")] public string ErrorType { get; set; }
    }
}