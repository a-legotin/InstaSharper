using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Tests
{
    public class UserInfoTest
    {
        public UserInfoTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;
        private readonly string _username = "alex_codegarage";
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");

        [Fact]
        public async void GetCurrentUserTest()
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
            var getUserResult = await apiInstance.GetCurrentUserAsync();
            var user = getUserResult.Value;
            //assert
            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, _username);
        }

        [Fact]
        public async void GetUserTest()
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
            var getUserResult = await apiInstance.GetUserAsync(_username);
            var user = getUserResult.Value;
            //assert
            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, _username);
        }
    }
}