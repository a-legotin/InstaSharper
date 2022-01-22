using System;
using System.Net;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.API.Services.User;
using InstaSharper.Http;
using InstaSharper.Models.Device;
using InstaSharper.Serialization;
using InstaSharper.Tests.Classes;
using InstaSharper.Utils;
using Moq;
using NUnit.Framework;

namespace InstaSharper.Tests.Unit.Services;

public class UserStateProviderTest : UnitTestBase
{
    [SetUp]
    public void SetupTest()
    {
        var cookies = new CookieContainer();
        cookies.Add(new Uri(Constants.BASE_URI), new Cookie(Constants.CSRFTOKEN, "my-token"));
        var httpHandler = new Mock<IHttpClientState>();
        httpHandler.Setup(state => state.GetCookieContainer())
            .Returns(cookies);

        _httpClientState = httpHandler.Object;
    }

    private IHttpClientState _httpClientState;

    [Test]
    public void MustThrowOnEmptyUser()
    {
        var userStateService = new UserStateService(new StreamSerializer(), _httpClientState,
            new AndroidDevice(Guid.NewGuid(), "my-device"), new AuthorizationHeaderProvider());
        Assert.Throws<Exception>(() => userStateService.GetStateDataAsByteArray(),
            "UserStateService must throw on empty user");
    }

    [Test]
    public void SaveStateTest()
    {
        var authHeaderProvider = new AuthorizationHeaderProvider
        {
            AuthorizationHeader = "auth-header"
        };
        var userStateService = new UserStateService(new StreamSerializer(), _httpClientState,
            new AndroidDevice(Guid.NewGuid(), "my-device"), authHeaderProvider);

        userStateService.SetUser(new InstaUserShort
        {
            Pk = 1234,
            UserName = "my-user"
        });

        var state = userStateService.GetStateDataAsByteArray();

        var newAuthHeaderProvider = new AuthorizationHeaderProvider();
        var newUserStateService = new UserStateService(new StreamSerializer(), _httpClientState,
            new AndroidDevice(Guid.NewGuid(), "another-device"), newAuthHeaderProvider);
        newUserStateService.LoadStateDataFromByteArray(state);

        Assert.AreEqual(userStateService.CsrfToken, newUserStateService.CsrfToken);
        Assert.AreEqual(userStateService.Device.DeviceId, newUserStateService.Device.DeviceId);
        Assert.AreEqual(userStateService.CurrentUser.Pk, newUserStateService.CurrentUser.Pk);
        Assert.AreEqual(authHeaderProvider.AuthorizationHeader, newAuthHeaderProvider.AuthorizationHeader);
    }
}