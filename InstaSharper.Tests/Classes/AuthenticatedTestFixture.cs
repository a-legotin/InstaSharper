using System;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;

namespace InstaSharper.Tests.Classes
{
    public class AuthenticatedTestFixture : IDisposable
    {
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public AuthenticatedTestFixture()
        {
            ApiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = _username,
                Password = _password
            });

            var loginTask = Task.Run(ApiInstance.LoginAsync);
            if (!loginTask.Wait(TimeSpan.FromSeconds(10)))
                throw new Exception($"Unable to login, user: {_username}, password: {_password}.");
        }

        public IInstaApi ApiInstance { get; }

        public void Dispose()
        {
            var logoutTask = Task.Run(ApiInstance.LogoutAsync);
            if (!logoutTask.Wait(TimeSpan.FromSeconds(10)))
                throw new Exception($"Not able to logout, user: {_username}, password: {_password}.");
        }

        public string GetUsername()
        {
            return _username;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}