using Newtonsoft.Json;

namespace InstaSharper.Classes
{
    internal class InstaLoginBaseResponse
    {
        #region InvalidCredentials

        [JsonProperty("invalid_credentials")]
        public bool InvalidCredentials { get; set; }

        [JsonProperty("error_type")]
        public string ErrorType { get; set; }

        #endregion

        #region 2 Factor Authentication

        [JsonProperty("two_factor_required")]
        public bool TwoFactorRequired { get; set; }

        [JsonProperty("two_factor_info")]
        public TwoFactorInfo TwoFactorInfo { get; set; }

        #endregion
    }

    public class TwoFactorInfo
    {
        [JsonProperty("obfuscated_phone_number")]
        public short ObfuscatedPhoneNumber { get; set; }

        [JsonProperty("show_messenger_code_option")]
        public bool ShowMessengerCodeOption { get; set; }

        [JsonProperty("two_factor_identifier")]
        public string TwoFactorIdentifier { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("phone_verification_settings")]
        public PhoneVerificationSettings PhoneVerificationSettings { get; set; }
    }

    public class PhoneVerificationSettings
    {
        [JsonProperty("max_sms_count")]
        public short MaxSmsCount { get; set; }

        [JsonProperty("resend_sms_delay_sec")]
        public int ResendSmsDelaySeconds { get; set; }

        [JsonProperty("robocall_after_max_sms")]
        public bool RobocallAfterMaxSms { get; set; }

        [JsonProperty("robocall_count_down_time")]
        public int RobocallCountDownTime { get; set; }
    }
}