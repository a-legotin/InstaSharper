using System.Linq;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration.Media;

public class FeedServiceTest : AuthenticatedTestBase
{
    [Test]
    public async Task Should_Load_First_Media_Feed_2Pages()
    {
        var userPk = 232192182;
        (await _api.Feed.GetUserFeedAsync(userPk, PaginationParameters.MaxPagesToLoad(2)))
            .Match(r =>
                {
                    Assert.Greater(r.Count, 0, "media list should not be empty");
                    Assert.IsFalse(r.GroupBy(x => x.Code).Any(g => g.Count() > 1),
                        "media list should not contain duplicates");
                },
                l => { Assert.Fail(l.Message); });
    }

    [Test]
    public async Task Should_Load_TimelineFeed()
    {
        (await _api.Feed.GetTimelineFeedAsync(PaginationParameters.MaxPagesToLoad(2)))
            .Match(r =>
                {
                    Assert.Greater(r.MediaItemsCount, 0, "feed should not be empty");
                    Assert.IsFalse(r.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1),
                        "feed medias should not contain duplicates");
                },
                l => { Assert.Fail(l.Message); });
    }
}