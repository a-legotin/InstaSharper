namespace InstaSharper.Classes
{
    public enum InstaLoginResult
    {
        Success,
        BadPassword,
        InvalidUser,
        TwoFactorRequired,
        ChallengeRequired,
        Exception
    }
}