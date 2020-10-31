using System;
using InstaSharper.Abstractions.Utils;
using InstaSharper.Device;

namespace InstaSharper.Abstractions.Device
{
    internal class AndroidDevice : IDevice
    {
        public AndroidDevice(Guid deviceId, string userAgent)
        {
            DeviceId = deviceId;
            UserAgent = userAgent;
            AndroidId = "android-" + CryptoHelper.CalculateMd5(deviceId.ToString()).Substring(0, 16);
        }

        public Guid DeviceId { get; }
        public string AndroidId { get; }
        public string UserAgent { get; }
    }
}