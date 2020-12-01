using System;
using System.Threading.Tasks;
using InstaSharper.Models.User;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration
{
    public class UserAuthenticationTest : IntegrationTestBase
    {
        [Test]
        public async Task UserLoginLogoutTest()
        {
            var credentials = new UserCredentials(
                Environment.GetEnvironmentVariable("instaapiusername"),
                Environment.GetEnvironmentVariable("instaapiuserpassword"));

            var api = Builder.Builder.Create()
                .WithUserCredentials(credentials)
                .Build();

            var loginResult = await api.User.LoginAsync();
            Assert.IsTrue(loginResult.IsRight);
            loginResult.Match(r => Assert.AreEqual(credentials.Username, r.UserName),
                l => { Assert.Fail(l.Message); });

            var logoutResult = await api.User.LogoutAsync();
            Assert.IsTrue(logoutResult.IsRight);
        }
    }
}