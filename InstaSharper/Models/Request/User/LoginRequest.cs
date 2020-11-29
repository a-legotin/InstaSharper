using System;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Infrastructure;
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;
using Newtonsoft.Json;

namespace InstaSharper.Models.Request.User
{
    internal class LoginRequest
    {
        [JsonProperty("jazoest")]
        public string Jazoest { get; set; }

        [JsonProperty("country_codes")]
        public string CountryCodes { get; set; } =
            "[{\"country_code\":\"1\",\"source\":[\"default\"]},{\"country_code\":\"1\",\"source\":[\"uig_via_phone_id\"]}]";

        [JsonProperty("phone_id")]
        public string PhoneId { get; set; }

        [JsonProperty("enc_password")]
        public string EncPassword { get; set; }

        [JsonProperty("_csrftoken")]
        public string CsrfToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("adid")]
        public string AdId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("google_tokens")]
        public string GoogleTokens { get; set; } = "[]";


        [JsonProperty("login_attempt_count")]
        public string LoginAttemptCount { get; set; } = "0";

        public static async Task<LoginRequest> Build(IDevice device,
            IUserCredentials credentials,
            ILauncherKeysProvider launcherKeysProvider,
            IPasswordEncryptor passwordEncryptor)
        {
            var time = DateTime.UtcNow.ToUnixTime();
            var (publicKey, keyId) = await launcherKeysProvider.GetKeysAsync();
            return new LoginRequest
            {
                Jazoest = device.Jazoest,
                Username = credentials.Username,
                DeviceId = device.DeviceId.ToString(),
                PhoneId = device.AndroidId,
                EncPassword =
                    $"#PWD_INSTAGRAM:4:{time}:{passwordEncryptor.EncryptPassword(credentials.Password, publicKey, keyId, time)}"
            };
        }
    }
}