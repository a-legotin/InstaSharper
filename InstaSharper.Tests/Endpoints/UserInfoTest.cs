using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class UserInfoTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public UserInfoTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyFact]
        public async void GetCurrentUserTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getUserResult = await _authInfo.ApiInstance.GetCurrentUserAsync();
            var user = getUserResult.Value;

            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, _authInfo.GetUsername());
        }

        [RunnableInDebugOnlyFact]
        public async void GetUserTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var username = _authInfo.GetUsername();

            var getUserResult = await _authInfo.ApiInstance.GetUserAsync(username);
            var user = getUserResult.Value;

            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }

        [RunnableInDebugOnlyFact]
        public async void SetAccountPrivacyTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var resultSetPrivate = await _authInfo.ApiInstance.SetAccountPrivateAsync();
            var resultSetPublic = await _authInfo.ApiInstance.SetAccountPublicAsync();

            Assert.True(resultSetPrivate.Succeeded);
            Assert.NotNull(resultSetPrivate.Value);
            Assert.True(resultSetPublic.Succeeded);
            Assert.NotNull(resultSetPrivate.Value);
        }

        [SkippableFact]
        public async void ChangePasswordTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var password = _authInfo.GetPassword();

            var resultChangePassword = await _authInfo.ApiInstance.ChangePasswordAsync(password, password);

            Assert.True(resultChangePassword.Succeeded);
            Assert.NotNull(resultChangePassword.Value);
        }
    }
}