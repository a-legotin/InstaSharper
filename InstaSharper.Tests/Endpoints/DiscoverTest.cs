using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class DiscoverTest : IClassFixture<AuthenticatedTestFixture>
    {
        public DiscoverTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        private readonly AuthenticatedTestFixture _authInfo;

        [Fact]
        public async void ExploreTest()
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);
            var result = await _authInfo.ApiInstance.GetExploreFeedAsync(2);
            var exploreGeed = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(exploreGeed);
        }
    }
}