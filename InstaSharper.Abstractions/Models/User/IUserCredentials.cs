namespace InstaSharper.Abstractions.Models.User
{
    public interface IUserCredentials
    {
        public string Username { get; }
        public string Password { get; }
    }
}