using System;
using InstaSharper.Abstractions.Device;
using Newtonsoft.Json;

namespace InstaSharper.Models.Request
{
    internal class LauncherSyncRequest
    {
        [JsonProperty("id")]
        public Guid DeviceId { get; set; }

        [JsonProperty("server_config_retrieval")]
        public int ServerConfigRetrieval { get; set; } = 1;

        internal static LauncherSyncRequest FromDevice(IDevice device)
            => new LauncherSyncRequest
            {
                DeviceId = device.DeviceId
            };
    }
}