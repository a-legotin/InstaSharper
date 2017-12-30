using System.Linq;
using InstaSharper.Tests.Classes;
using Xunit;

namespace InstaSharper.Tests.Endpoints
{
    [Trait("Category", "Endpoint")]
    public class LocationTest : IClassFixture<AuthenticatedTestFixture>
    {
        private readonly AuthenticatedTestFixture _authInfo;

        public LocationTest(AuthenticatedTestFixture authInfo)
        {
            _authInfo = authInfo;
        }

        [Theory]
        [InlineData("winter")]
        public async void SearchLocation(string searchQuery)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result = await _authInfo.ApiInstance.SearchLocation(59.9384401, 30.3162374, searchQuery);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Any(location => location.Name.ToLowerInvariant().Contains(searchQuery)));
        }
    }
}