using InstaSharper.Abstractions.Models.User;

namespace InstaSharper.Models.User;

internal class UserCredentials : IUserCredentials
{
    public UserCredentials(string username,
                           string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}