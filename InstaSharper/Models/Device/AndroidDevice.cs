using System;
using InstaSharper.Abstractions.Device;
using InstaSharper.Utils;

namespace InstaSharper.Models.Device;

[Serializable]
internal class AndroidDevice : IDevice
{
    public AndroidDevice(Guid deviceId,
                         string userAgent,
                         string xigCapabilities)
    {
        DeviceId = deviceId;
        UserAgent = userAgent;
        XigCapabilities = xigCapabilities;
        AndroidId = "android-" + CryptoHelper.CalculateMd5(deviceId.ToString()).Substring(0, 16);
        Jazoest = InstaUtils.GenerateJazoest(deviceId);
    }

    public AndroidDevice(Guid deviceId,
                         string userAgent) : this(deviceId, userAgent, string.Empty)
    {
    }

    public string Jazoest { get; }
    public Guid DeviceId { get; }
    public string AndroidId { get; }
    public string UserAgent { get; }
    public string XigCapabilities { get; }
    public string PigeonSessionId { get; set; } = Guid.NewGuid().ToString();
}