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
        (await _api.Followers.GetUserFollowersAsync(userPk, PaginationParameters.Empty))
            .Match(r => { Assert.Greater(r.Count, 0, "PK must be greater 0"); },
                l => { Assert.Fail(l.Message); });
    }
}