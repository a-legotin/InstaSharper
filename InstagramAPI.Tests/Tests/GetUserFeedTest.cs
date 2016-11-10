using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.Tests.Utils;
using Xunit;

namespace InstagramAPI.Tests.Tests
{
    public class GetUserFeedTest
    {
        [Fact]
        public async void GetUserPostsTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = "3591957P@R";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials
                {
                    UserName = username,
                    Password = password
                });
            //act
            var success = await apiInstance.LoginAsync();
            var posts = await apiInstance.GetUserPostsAsync(username);
            //assert
            Assert.True(success);
        }
    }
}
