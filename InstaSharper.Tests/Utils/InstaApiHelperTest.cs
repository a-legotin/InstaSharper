using InstaSharper.Helpers;
using Xunit;

namespace InstaSharper.Tests.Utils
{
    [Collection("Infrastructure")]
    public class InstaApiHelperTest
    {
        [Fact]
        public void GetCodeFromIdTest()
        {
            var expectedCode = "BQiSd7KFk9r";
            var sourceId = 1450803251188748139;

            var actualCode = InstaApiHelper.GetCodeFromId(sourceId);
            Assert.Equal(expectedCode, actualCode);
        }
    }
}