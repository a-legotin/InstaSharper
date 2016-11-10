using InstagramAPI.API.Builder;
using InstagramAPI.Tests.Utils;
using Xunit;

namespace InstagramAPI.Tests.Tests
{
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