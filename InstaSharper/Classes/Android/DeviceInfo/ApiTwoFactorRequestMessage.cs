using InstaSharper.API;
using InstaSharper.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes.Android.DeviceInfo
{
    internal class ApiTwoFactorRequestMessage
    {
        public string verification_code { get; set; }
        public string username { get; set; }
        public string device_id { get; set; }
        public string two_factor_identifier { get; set; }


        internal string GenerateSignature(string signatureKey)
        {
            if (string.IsNullOrEmpty(signatureKey))
                signatureKey = InstaApiConstants.IG_SIGNATURE_KEY;
            return CryptoHelper.CalculateHash(signatureKey,
                JsonConvert.SerializeObject(this));
        }

        internal string GetMessageString() => JsonConvert.SerializeObject(this);

        internal ApiTwoFactorRequestMessage(string verificationCode, string username, string deviceId, string twoFactorIdentifier)
        {
            verification_code = verificationCode;
            this.username = username;
            device_id = deviceId;
            two_factor_identifier = twoFactorIdentifier;
        }
    }
}
