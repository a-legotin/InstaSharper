using System.Linq;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class FollowersTest : IClassFixture<AuthenticatedTestFixture>
    {
        public FollowersTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Theory]
        [InlineData("therock")]
        public async void GetUserFollowersTest(string username)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetUserFollowersAsync(username, 10);
            var followers = result.Value;
            var anyDuplicate = followers.GroupBy(x => x.Pk).Any(g => g.Count() > 1);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
            Assert.False(anyDuplicate);
        }

        [Theory]
        [InlineData("therock")]
        public async void GetUserFollowingTest(string username)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetUserFollowingAsync(username, 10);
            var followers = result.Value;
            var anyDuplicate = followers.GroupBy(x => x.Pk).Any(g => g.Count() > 1);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
            Assert.False(anyDuplicate);
        }

        [Theory]
        [InlineData(196754384)]
        public async void FollowUnfollowUserTest(long userId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var followResult = await _authInfo.ApiInstance.FollowUserAsync(userId);
            var unFollowResult = await _authInfo.ApiInstance.UnFollowUserAsync(userId);
            //assert
            Assert.True(followResult.Succeeded);
            Assert.True(unFollowResult.Succeeded);

            Assert.True(followResult.Value.Following);
            Assert.False(unFollowResult.Value.Following);
        }

        [Fact]
        public async void GetCurrentUserFollwersTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetCurrentUserFollowersAsync();
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }
    }
}