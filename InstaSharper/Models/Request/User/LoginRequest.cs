using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Device;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Infrastructure;
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;

namespace InstaSharper.Models.Request.User;

internal class LoginRequest
{
    [JsonPropertyName("jazoest")]
    public string Jazoest { get; set; }

    [JsonPropertyName("country_codes")]
    public string CountryCodes { get; set; } =
        "[{\"country_code\":\"1\",\"source\":[\"default\"]},{\"country_code\":\"1\",\"source\":[\"uig_via_phone_id\"]}]";

    [JsonPropertyName("phone_id")]
    public string PhoneId { get; set; }

    [JsonPropertyName("enc_password")]
    public string EncPassword { get; set; }

    [JsonPropertyName("_csrftoken")]
    public string CsrfToken { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("adid")]
    public string AdId { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("google_tokens")]
    public string GoogleTokens { get; set; } = "[]";


    [JsonPropertyName("login_attempt_count")]
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