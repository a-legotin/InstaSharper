using System.Threading.Tasks;
using NUnit.Framework;

namespace InstaSharper.Tests
{
    public class UserServiceTest : IntegrationTestBase
    {
        [Test]
        public async Task UserLoginTest()
        {
            var credentials = GetUserCredentials();
            var api = Builder.Builder.Create()
                .WithUserCredentials(credentials)
                .Build();

            var result = await api.User.LoginAsync();
            Assert.IsTrue(result.IsRight);
            result.Match(r => Assert.AreEqual(credentials.Username, r.UserName),
                l => { Assert.Fail(l.Message); });
        }
    }
}