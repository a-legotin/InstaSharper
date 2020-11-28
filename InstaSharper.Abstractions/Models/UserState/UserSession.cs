using System;
using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Abstractions.Models.UserState
{
    [Serializable]
    public class UserSession
    {
        public InstaUserShort LoggedInUser { get; set; }
        public string RankToken { get; set; }
        public string CsrfToken { get; set; }
    }
}