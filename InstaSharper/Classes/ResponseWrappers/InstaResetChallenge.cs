using Newtonsoft.Json;

namespace InstaSharper.Classes.ResponseWrappers
{
    public class InstaResetChallenge
    {
        [JsonProperty("step_name")] public string StepName { get; set; }

        [JsonProperty("step_data")] public InstaStepData StepData { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("nonce_code")] public string NonceCode { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("logged_in_user")] public InstaUserResponse LoggedInUser { get; set; }

        [JsonProperty("action")] public string Action { get; set; }

        [JsonProperty("auto_login")] public bool AutoLogin { get; set; }
    }
}