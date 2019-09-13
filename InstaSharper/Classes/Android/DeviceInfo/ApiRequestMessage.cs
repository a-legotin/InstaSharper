using System;
using InstaSharper.API;
using InstaSharper.Helpers;
using Newtonsoft.Json;

namespace InstaSharper.Classes.Android.DeviceInfo
{
    public class ApiRequestMessage
    {
        public string phone_id { get; set; }
        public string username { get; set; }
        public Guid guid { get; set; }
        public string device_id { get; set; }
        public string password { get; set; }
        public string login_attempt_count { get; set; } = "0";

        internal string GetMessageString()
        {
            return JsonConvert.SerializeObject(this);
        }

        internal string GenerateSignature(string signatureKey)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = InstaApiConstants.IG_SIGNATURE_KEY;
            return CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(this));
        }

        internal bool IsEmpty()
        {
            if (string.IsNullOrEmpty(phone_id)) return true;
            if (string.IsNullOrEmpty(device_id)) return true;
            if (Guid.Empty == guid) return true;
            return false;
        }

        internal static string GenerateDeviceId()
        {
            return GenerateDeviceIdFromGuid(Guid.NewGuid());
        }

        internal static string GenerateUploadId()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            var uploadId = (long) timeSpan.TotalSeconds;
            return uploadId.ToString();
        }

        public static ApiRequestMessage FromDevice(AndroidDevice device)
        {
            var requestMessage = new ApiRequestMessage
            {
                phone_id = device.PhoneGuid.ToString(),
                guid = device.DeviceGuid,
                device_id = device.DeviceId
            };
            return requestMessage;
        }

        public static string GenerateDeviceIdFromGuid(Guid guid)
        {
            var hashedGuid = CryptoHelper.CalculateMd5(guid.ToString());
            return $"android-{hashedGuid.Substring(0, 16)}";
        }
    }
}