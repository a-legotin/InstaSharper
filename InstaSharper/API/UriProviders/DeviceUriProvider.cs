using System;
using System.Collections.Specialized;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class DeviceUriProvider : IDeviceUriProvider
{
    private static readonly string ZrToken = "zr/token/result/";
    public Uri SyncLauncher { get; } = new(Constants.BASE_URI_APIv1 + "launcher/sync/");

    public Uri GetZrToken(string deviceId,
                          string customDeviceId)
    {
        var queryParams = new NameValueCollection
        {
            { "device_id", deviceId },
            { "token_hash", "" },
            { "custom_device_id", customDeviceId },
            { "fetch_reason", "token_expired" }
        };
        if (!Uri.TryCreate(new Uri(Constants.BASE_URI_APIv1), ZrToken,
                out var result))
            throw new Exception("Cant create ZR token result URI");

        return new UriBuilder(result)
        {
            Query = queryParams.ToQueryString()
        }.Uri;
    }
}