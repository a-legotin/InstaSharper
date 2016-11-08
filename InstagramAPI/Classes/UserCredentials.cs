namespace InstagramApi.Classes
{
    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUser LoggedInUder { get; set; }

        public string RankToken { get; set; }

    }
}