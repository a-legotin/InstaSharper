using System.Threading.Tasks;
using InstaSharper.API.Services;

namespace InstaSharper.Infrastructure;

internal class LauncherKeysProvider : ILauncherKeysProvider
{
    private readonly IDeviceService _deviceService;

    public LauncherKeysProvider(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    private string PublicKey { get; set; }
    private string KeyId { get; set; }

    public async Task<(string publicKey, string keyId)> GetKeysAsync()
    {
        if (string.IsNullOrEmpty(PublicKey)
            || string.IsNullOrEmpty(KeyId))
            (await _deviceService.LauncherSyncAsync())
                .Match(r =>
                {
                    PublicKey = r.PublicKey;
                    KeyId = r.KeyId;
                }, _ => { });

        return (PublicKey, KeyId);
    }
}