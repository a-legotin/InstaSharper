using InstagramApi.API.Builder;
using InstagramApi.Tests.Utils;
using Xunit;

namespace InstagramApi.Tests.Tests
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