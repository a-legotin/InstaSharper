using InstagramApi.API.Builder;
using Xunit;

namespace InstagramApi.Tests
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