using System;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;


namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    class StoryTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = System.IO.File.ReadAllText(@"C:\privKey\instasharp.txt");
        private readonly string _username = "thisidlin";

        public StoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyFact]
        private async void GetStoryTray()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = TestHelpers.Login(apiInstance, _output);
            Assert.True(loginSucceed);
            var result = await apiInstance.GetStoryTrayAsync();
            var stories = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }

        [RunnableInDebugOnlyTheory]
        [InlineData(1129166614)]
        private async void GetUserStory(long userId)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = TestHelpers.Login(apiInstance, _output);
            Assert.True(loginSucceed);
            var result = await apiInstance.GetUserStoryAsync(userId);
            var stories = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(stories);
        }
    }
}
