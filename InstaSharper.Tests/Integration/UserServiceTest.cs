using System;
using System.Threading.Tasks;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration;

public class UserServiceTest : AuthenticatedTestBase
{
    [Test]
    public async Task Should_Find_Exact_User()
    {
        var username = "design";
        (await _api.User.GetUserAsync(username))
            .Match(r =>
                {
                    Assert.Greater(r.Pk, 0, "PK must be greater 0");
                    Assert.AreEqual(username, r.UserName);
                },
                l => { Assert.Fail(l.Message); });
    }

    [Test]
    public async Task Should_Find_List_Of_Users()
    {
        var query = "des";
        (await _api.User.SearchUsersAsync(query))
            .Match(r =>
                {
                    foreach (var user in r)
                        Assert.IsTrue(user.UserName.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                                      || user.FullName.Contains(query,
                                          StringComparison.InvariantCultureIgnoreCase));
                },
                l => { Assert.Fail(l.Message); });
    }
}