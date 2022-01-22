using System;
using System.Text.Json.Serialization;
using InstaSharper.Abstractions.Device;

namespace InstaSharper.Models.Request.System;

internal class LauncherSyncRequest
{
    [JsonPropertyName("id")]
    public Guid DeviceId { get; set; }

    [JsonPropertyName("server_config_retrieval")]
    public int ServerConfigRetrieval { get; set; } = 1;

    internal static LauncherSyncRequest FromDevice(IDevice device)
    {
        return new LauncherSyncRequest
        {
            DeviceId = device.DeviceId
        };
    }
}