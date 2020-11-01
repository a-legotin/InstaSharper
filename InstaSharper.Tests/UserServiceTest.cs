using System.Threading.Tasks;
using NUnit.Framework;

namespace InstaSharper.Tests
{
    [TestFixture]
    [Category("Integration")]
    public class UserServiceTest
    {
        [Test]
        public async Task UserLoginTest()
        {
            var api = Builder.Builder.Create()
                .WithUserCredentials("boner.bungee", "")
                .Build();
            var result = await api.User.LoginAsync();
            Assert.IsTrue(result.IsRight);
        }
    }
}