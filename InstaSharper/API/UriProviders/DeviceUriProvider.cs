using System;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Utils;

namespace InstaSharper.API.UriProviders;

internal class DeviceUriProvider : IDeviceUriProvider
{
    public Uri SyncLauncher { get; } = new(Constants.BASE_URI_APIv1 + "launcher/sync/");
}