using System;
using System.Text.Json.Serialization;
using InstaSharper.Abstractions.Device;

namespace InstaSharper.Models.Request.User
{
    internal class LogoutRequest
    {
        [JsonPropertyName("phone_id")]
        public string PhoneGuid { get; set; }

        [JsonPropertyName("_csrftoken")]
        public string CsrfToken { get; set; }

        [JsonPropertyName("guid")]
        public Guid Guid  => DeviceGuid;

        [JsonPropertyName("device_id")]
        public Guid DeviceId { get; set; }

        [JsonPropertyName("_uuid")]
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