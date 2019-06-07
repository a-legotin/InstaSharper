using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class HashtagTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public HashtagTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [Theory]
        [InlineData("christmas")]
        public async void SearchHashtag(string searchQuery)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.SearchHashtag(searchQuery);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.Contains(result.Value, hashtag => hashtag.Name.ToLowerInvariant().Contains(searchQuery));
        }

        [Theory]
        [InlineData("christmas")]
        public async void GetHashtagInfo(string tagname)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.GetHashtagInfo(tagname);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.Equal(tagname, result.Value.Name.ToLowerInvariant());
        }
    }
}