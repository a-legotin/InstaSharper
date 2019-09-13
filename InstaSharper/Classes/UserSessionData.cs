using InstaSharper.Classes.Models;

namespace InstaSharper.Classes
{
    public class UserSessionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUser LoggedInUser { get; set; }

        public string RankToken { get; set; }
        public string CsrfToken { get; set; }
    }
}
