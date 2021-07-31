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
        public async Task Must_Login_And_Save_LoadState()
        {
            var credentials = new UserCredentials(
                Environment.GetEnvironmentVariable("instaapiusername"),
                Environment.GetEnvironmentVariable("instaapiuserpassword"));

            var api = Builder.Builder.Create()
                .WithUserCredentials(credentials)
                .Build();

            var loginResult = await api.User.LoginAsync();
            Assert.IsTrue(loginResult.IsRight);
            Assert.IsTrue(api.User.IsAuthenticated);
            loginResult.Match(r => Assert.AreEqual(credentials.Username, r.UserName),
                l => { Assert.Fail(l.Message); });

            var state = api.User.GetUserSessionAsByteArray();

            var logoutResult = await api.User.LogoutAsync();
            Assert.IsTrue(logoutResult.IsRight);
            Assert.IsFalse(api.User.IsAuthenticated);

            api = Builder.Builder.Create()
                .WithUserSession(state)
                .Build();
            var me = await api.User.GetUserAsync(credentials.Username);

            Assert.IsTrue(me.IsRight);
        }

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
            Assert.IsTrue(api.User.IsAuthenticated);
            loginResult.Match(r => Assert.AreEqual(credentials.Username, r.UserName),
                l => { Assert.Fail(l.Message); });

            var logoutResult = await api.User.LogoutAsync();
            Assert.IsTrue(logoutResult.IsRight);
        }
    }
}