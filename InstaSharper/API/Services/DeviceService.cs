using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models;
using InstaSharper.Abstractions.Models.Status;
using InstaSharper.Http;
using InstaSharper.Models.Request.System;
using LanguageExt;

namespace InstaSharper.API.Services
{
    internal class DeviceService : IDeviceService
    {
        private static readonly string PUB_KEY_HEADER = "ig-set-password-encryption-pub-key";
        private static readonly string KEY_ID_HEADER = "ig-set-password-encryption-key-id";
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
                PublicKey = string.Join("", message.Headers.GetValues(PUB_KEY_HEADER)),
                KeyId = string.Join("", message.Headers.GetValues(KEY_ID_HEADER))
            });
        }
    }
}