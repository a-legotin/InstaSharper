using InstaSharper.API.Builder;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Tests
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