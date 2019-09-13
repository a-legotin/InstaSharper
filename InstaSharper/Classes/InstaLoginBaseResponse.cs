﻿using InstaSharper.Classes.ResponseWrappers;
using Newtonsoft.Json;

namespace InstaSharper.Classes
{
    internal class InstaLoginBaseResponse
    {
        #region InvalidCredentials

        [JsonProperty("invalid_credentials")] public bool InvalidCredentials { get; set; }

        [JsonProperty("error_type")] public string ErrorType { get; set; }

        #endregion

        #region 2 Factor Authentication

        [JsonProperty("two_factor_required")] public bool TwoFactorRequired { get; set; }

        [JsonProperty("two_factor_info")] public TwoFactorLoginInfo TwoFactorLoginInfo { get; set; }

        #endregion

        #region Challenge Required

        [JsonIgnore] public bool ChallengeRequired => ErrorType == "checkpoint_challenge_required";
        
        [JsonProperty("challenge")] public InstaChallenge Challenge { get; set; }
        
        #endregion
    }
}