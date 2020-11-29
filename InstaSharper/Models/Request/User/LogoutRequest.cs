using System;
using InstaSharper.Abstractions.Device;
using Newtonsoft.Json;

namespace InstaSharper.Models.Request.User
{
    internal class LogoutRequest
    {
        [JsonProperty("phone_id")]
        public string PhoneGuid { get; set; }

        [JsonProperty("_csrftoken")]
        public string CsrfToken { get; set; }

        [JsonProperty("guid")]
        public Guid Guid  => DeviceGuid;

        [JsonProperty("device_id")]
        public Guid DeviceId { get; set; }

        [JsonProperty("_uuid")]
        public Guid DeviceGuid { get; set; }

        public static LogoutRequest Build(IDevice device, string csrfToken) =>
            new LogoutRequest
            {
                CsrfToken = csrfToken,
                DeviceGuid = device.DeviceId,
                PhoneGuid = device.AndroidId
            };
    }
}