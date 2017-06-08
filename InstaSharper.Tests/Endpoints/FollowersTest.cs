using System.Linq;
using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class FollowersTest : IClassFixture<AuthenticatedTestFixture>
    {
        readonly AuthenticatedTestFixture _authInfo;

        public FollowersTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyTheory]
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

        [RunnableInDebugOnlyFact]
        public async void GetCurrentUserFollwersTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetCurrentUserFollowersAsync();
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }

        [RunnableInDebugOnlyTheory]
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
    }
}