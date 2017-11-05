using InstaSharper.API.Builder;
using InstaSharper.Logger;
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
                .UseLogger(new DebugLogger(LogLevel.All))
                .Build();
            Assert.NotNull(result);
        }
    }
}