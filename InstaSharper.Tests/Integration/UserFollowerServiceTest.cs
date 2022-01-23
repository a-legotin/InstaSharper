using System.Linq;
using System.Threading.Tasks;
using InstaSharper.Abstractions.Models;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration;

public class UserFollowerServiceTest : AuthenticatedTestBase
{
    [Test]
    public async Task GetUserFollowersTest()
    {
        var userPk = 232192182;
        (await _api.Followers.GetUserFollowersAsync(userPk, PaginationParameters.MaxPagesToLoad(2)))
            .Match(r =>
                {
                    Assert.Greater(r.Count, 0, "PK must be greater 0");
                    Assert.IsFalse(r.GroupBy(x => x.Pk).Any(g => g.Count() > 1),
                        "followers list should not contain duplicates");
                },
                l => { Assert.Fail(l.Message); });
    }

    [Test]
    public async Task GetUserFollowingTest()
    {
        var userPk = 232192182;
        (await _api.Followers.GetUserFollowingAsync(userPk, PaginationParameters.MaxPagesToLoad(5)))
            .Match(r =>
                {
                    Assert.Greater(r.Count, 0, "following count should be greater 0");
                    Assert.IsFalse(r.GroupBy(x => x.Pk).Any(g => g.Count() > 1),
                        "following list should not contain duplicates");
                },
                l => { Assert.Fail(l.Message); });
    }
}