using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class UserInfoTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public UserInfoTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyFact]
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

        [RunnableInDebugOnlyFact]
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

        [RunnableInDebugOnlyFact]
        public async void SetAccountPrivacyTest()
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
            var resultSetPrivate = await apiInstance.SetAccountPrivateAsync();
            var resultSetPublic = await apiInstance.SetAccountPublicAsync();
            //assert
            Assert.True(resultSetPrivate.Succeeded);
            Assert.NotNull(resultSetPrivate.Value);
            Assert.True(resultSetPublic.Succeeded);
            Assert.NotNull(resultSetPrivate.Value);
        }

        [RunnableInDebugOnlyFact]
        public async void ChangePasswordTest()
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
            var resultChangePassword = await apiInstance.ChangePasswordAsync("oldPassword", "newPassword");
            //assert
            Assert.True(resultChangePassword.Succeeded);
            Assert.NotNull(resultChangePassword.Value);
        }
    }
}