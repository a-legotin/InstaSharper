using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class UserInfoTest : IClassFixture<AuthenticatedTestFixture>
    {
        public UserInfoTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [SkippableFact]
        public async void ChangePasswordTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var password = _authInfo.GetPassword();

            var resultChangePassword = await _authInfo.ApiInstance.ChangePasswordAsync(password, password);

            Assert.True(resultChangePassword.Succeeded);
            Assert.NotNull(resultChangePassword.Value);
        }

        [Theory]
        [InlineData(196754384)]
        public async void GetFriendshipStatusTest(long userId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetFriendshipStatusAsync(userId);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async void GetCurrentUserTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var getUserResult = await _authInfo.ApiInstance.GetCurrentUserAsync();
            var user = getUserResult.Value;

            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, _authInfo.GetUsername());
        }

        [Theory]
        [InlineData("therock")]
        public async void GetUserTest(string username)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var getUserResult = await _authInfo.ApiInstance.GetUserAsync(username);
            var user = getUserResult.Value;

            Assert.True(getUserResult.Succeeded);
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }

        [Fact]
        public async void SetAccountPrivacyTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var resultSetPrivate = await _authInfo.ApiInstance.SetAccountPrivateAsync();
            var resultSetPublic = await _authInfo.ApiInstance.SetAccountPublicAsync();

            Assert.True(resultSetPrivate.Succeeded);
            Assert.NotNull(resultSetPrivate.Value);
            Assert.True(resultSetPrivate.Value.IsPrivate);

            Assert.True(resultSetPublic.Succeeded);
            Assert.NotNull(resultSetPublic.Value);
            Assert.False(resultSetPublic.Value.IsPrivate);
        }
    }
}