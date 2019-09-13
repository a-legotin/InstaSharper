using System.Linq;
using InstaSharper.Classes;
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
            Assert.Contains(result.Value, location => location.Name.ToLowerInvariant().Contains(searchQuery));
        }

        [Theory]
        [InlineData(109408589078990)]
        public async void GetLocationFeed(long locationId)
        {
            Assert.True(_authInfo.ApiInstance.IsUserAuthenticated);

            var result =
                await _authInfo.ApiInstance.GetLocationFeed(locationId, PaginationParameters.MaxPagesToLoad(15));
            var locationFeed = result.Value;
            var anyMediaDuplicate = locationFeed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyRankedDuplicate = locationFeed.RankedMedias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.False(anyMediaDuplicate);
            Assert.False(anyRankedDuplicate);
        }
    }
}