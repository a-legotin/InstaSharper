using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    [Collection("InstaSharper Tests")]
    public class DiscoverTest
    {
        public DiscoverTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        private readonly string _username = "alex_codegarage";
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");

        [Fact]
        public async void ExploreTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetExploreFeedAsync(0);
            var exploreGeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(exploreGeed);
        }
    }
}