using System.Threading.Tasks;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration
{
    public class UserServiceTest : IntegrationTestBase
    {
        [Test]
        public async Task UserLoginLogoutTest()
        {
            var credentials = GetUserCredentials();
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