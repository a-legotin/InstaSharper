using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Http;
using InstaSharper.Models.Request.System;
using InstaSharper.Utils;
using LanguageExt;

namespace InstaSharper.API.Services;

internal class DeviceService : IDeviceService
{
    private readonly IDevice _device;
    private readonly IDeviceUriProvider _deviceUriProvider;
    private readonly IInstaHttpClient _httpClient;

    public DeviceService(IDeviceUriProvider deviceUriProvider,
        IInstaHttpClient httpClient,
        IDevice device)
    {
        _deviceUriProvider = deviceUriProvider;
        _httpClient = httpClient;
        _device = device;
    }

    public async Task<Either<ResponseStatusBase, LauncherSyncResponse>> LauncherSyncAsync()
    {
        var response = await _httpClient.PostAsync(_deviceUriProvider.SyncLauncher,
            LauncherSyncRequest.FromDevice(_device));
        return response.Map(message => new LauncherSyncResponse
        {
            PublicKey = string.Join("", message.Headers.GetValues(Constants.Headers.PUB_KEY_HEADER)),
            KeyId = string.Join("", message.Headers.GetValues(Constants.Headers.KEY_ID_HEADER)),
            ShbId = string.Join("", message.Headers.GetValues(Constants.Headers.IG_SET_U_SHBID)),
            ShbTs = string.Join("", message.Headers.GetValues(Constants.Headers.IG_SET_U_SHBTS)),
            Rur = string.Join("", message.Headers.GetValues(Constants.Headers.IG_SET_U_RUR)),
        });
    }
}