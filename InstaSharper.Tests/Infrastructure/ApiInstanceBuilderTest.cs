using InstaSharper.API.Builder;
using InstaSharper.Tests.Utils;
using Xunit;

namespace InstaSharper.Tests.Infrastructure
{
    [Trait("Category", "Infrastructure")]
    public class ApiInstanceBuilderTest
    {
        [Fact]
        public void CreateApiInstanceWithBuilder()
        {
            var result = InstaApiBuilder.CreateBuilder()
                .UseLogger(new TestLogger())
                .Build();
            Assert.NotNull(result);
        }
    }
}