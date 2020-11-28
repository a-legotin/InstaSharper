using System;
using InstaSharper.Abstractions.Device;
using InstaSharper.Utils;

namespace InstaSharper.Models.Device
{
    [Serializable]
    internal class AndroidDevice : IDevice
    {
        public AndroidDevice(Guid deviceId, string userAgent)
        {
            DeviceId = deviceId;
            UserAgent = userAgent;
            AndroidId = "android-" + CryptoHelper.CalculateMd5(deviceId.ToString()).Substring(0, 16);
            Jazoest = InstaUtils.GenerateJazoest(deviceId);
        }

        public string Jazoest { get; }
        public Guid DeviceId { get; }
        public string AndroidId { get; }
        public string UserAgent { get; }
    }
}