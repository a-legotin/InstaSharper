using System;
using InstaSharper.Classes.Models;

namespace InstaSharper.Classes
{
    [Serializable]
    public class UserSessionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUserShort LoggedInUder { get; set; }

        public string RankToken { get; set; }
        public string CsrfToken { get; set; }
    }
}