using InstaSharper.API.Builder;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.FactNTheories
{
    [Collection("InstaSharper Tests")]
    public class ApiInstanceBuilderTest
    {
        [Fact]
        public void CreateApiInstanceWithBuilder()
        {
            var result = new InstaApiBuilder()
                .UseLogger(new TestLogger())
                .Build();
            Assert.NotNull(result);
        }
    }
}