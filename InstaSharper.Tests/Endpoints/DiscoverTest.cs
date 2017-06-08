using InstaSharper.Tests.Classes;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class DiscoverTest : IClassFixture<AuthenticatedTestFixture>
    {
        readonly AuthenticatedTestFixture _authInfo;

        public DiscoverTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [RunnableInDebugOnlyFact]
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