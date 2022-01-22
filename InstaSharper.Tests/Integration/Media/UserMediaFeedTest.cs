using System.Linq;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration.Media;

public class UserMediaFeedTest: AuthenticatedTestBase
{
    [Test]
    public async Task Should_Load_First_Media_Feed_Page()
    {
        var userPk = 232192182;
        (await _api.Feed.GetUserFeedAsync(userPk, PaginationParameters.MaxPagesToLoad(2)))
            .Match(r =>
                {
                    Assert.Greater(r.Count, 0, "media list should not be empty");
                    Assert.IsFalse(r.GroupBy(x => x.Code).Any(g => g.Count() > 1), "media list should not contain duplicates");
                },
                l => { Assert.Fail(l.Message); });
    }
}