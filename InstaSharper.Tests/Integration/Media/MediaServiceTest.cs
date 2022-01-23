using System.Threading.Tasks;
using InstaSharper.Tests.Classes;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration.Media;

public class MediaServiceTest : AuthenticatedTestBase
{
    [Test]
    public async Task Should_Load_MediasByIdList()
    {
        (await _api.Media.GetMediaListByIdsAsync("2753359181161297404_1451036219", "2732181426863779408_17830270841"))
            .Match(r => { Assert.AreEqual(r.Count, 2, "media list should contain two elements"); },
                l => { Assert.Fail(l.Message); });
    }
}