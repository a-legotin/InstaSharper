using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API.Services;
using InstaSharper.Abstractions.API.UriProviders;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.API.Services;
using InstaSharper.Http;
using InstaSharper.Infrastructure;
using InstaSharper.Infrastructure.Converters;
using InstaSharper.Infrastructure.Converters.User;
using InstaSharper.Models.Device;
using InstaSharper.Models.Request.User;
using InstaSharper.Models.Response.User;
using InstaSharper.Tests.Classes;
using InstaSharper.Utils;
using InstaSharper.Utils.Encryption;
using Moq;
using NUnit.Framework;

namespace InstaSharper.Tests.Unit.Services;

public class UserServiceTest : UnitTestBase
{
    [SetUp]
    public void SetupTest()
    {
        const string keyId = @"LS0tLS1CRUdJTiBQVUJMSUMgS==";
        var apiStateProvider = new Mock<IApiStateProvider>();
        var credentials = new Mock<IUserCredentials>();
        var httpClient = new Mock<IInstaHttpClient>();
        var launcherKeysProvider = new Mock<ILauncherKeysProvider>();
        var uriProvider = new Mock<IUserUriProvider>();
        var passwordEncryptor = new Mock<IPasswordEncryptor>();
        var userStateService = new Mock<IUserStateService>();
        var deviceServiceMock = new Mock<IDeviceService>();
        apiStateProvider.Setup(provider => provider.Device)
            .Returns(() => new AndroidDevice(Guid.NewGuid(), "some-device"));
        apiStateProvider.Setup(provider => provider.SetUser(It.IsAny<InstaUserShort>()))
            .Callback<InstaUserShort>(user => _currentUser = user);
        httpClient.Setup(client =>
                client.PostAsync<InstaLoginResponse, LoginRequest>(It.IsAny<Uri>(), It.IsAny<LoginRequest>()))
            .Returns(async () =>
                await Task.FromResult(new InstaLoginResponse
                {
                    Status = "ok",
                    User = _testUser
                })
            );

        httpClient.Setup(client =>
                client.PostAsync<InstaLogoutResponse, LogoutRequest>(It.IsAny<Uri>(), It.IsAny<LogoutRequest>()))
            .Returns(async () =>
                await Task.FromResult(new InstaLogoutResponse
                {
                    Status = "ok"
                })
            );
        credentials.Setup(userCredentials => userCredentials.Username)
            .Returns("username");
        credentials.Setup(userCredentials => userCredentials.Password)
            .Returns("password");
        launcherKeysProvider.Setup(provider => provider.GetKeysAsync())
            .Returns(async () =>
                await Task.FromResult((Convert.ToBase64String(Encoding.UTF8.GetBytes(keyId)), "203")));
        uriProvider.Setup(provider => provider.Login)
            .Returns(() => new Uri("http://dummy.com/login"));
        uriProvider.Setup(provider => provider.Logout)
            .Returns(() => new Uri("http://dummy.com/logout"));

        var cookies = new CookieContainer();
        cookies.Add(new Uri(Constants.BASE_URI), new Cookie(Constants.CSRFTOKEN, "my-token"));
        var httpHandler = new Mock<IHttpClientState>();
        httpHandler.Setup(state => state.GetCookieContainer())
            .Returns(cookies);
        passwordEncryptor.Setup(encryptor =>
                encryptor.EncryptPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<long>()))
            .Returns(() => "encryptedPassword");

        _apiStateProvider = apiStateProvider.Object;
        _httpClient = httpClient.Object;
        _credentials = credentials.Object;
        _launcherKeysProvider = launcherKeysProvider.Object;
        _uriProvider = uriProvider.Object;
        _userStateService = userStateService.Object;
        _deviceService = deviceServiceMock.Object;
        
        var userShortConverter = new UserShortConverter();
        _converters = new UserConverters(userShortConverter,
            new UserConverter(userShortConverter, new InstaFriendshipShortStatusConverter()));
        _passwordEncryptor = passwordEncryptor.Object;
    }

    private IApiStateProvider _apiStateProvider;
    private IUserStateService _userStateService;
    private IUserConverters _converters;
    private IUserCredentials _credentials;
    private IInstaHttpClient _httpClient;
    private ILauncherKeysProvider _launcherKeysProvider;
    private IUserUriProvider _uriProvider;
    private InstaUserShort _currentUser;
    private IPasswordEncryptor _passwordEncryptor;
    private IDeviceService _deviceService;
    
    private readonly InstaUserShortResponse _testUser = new()
    {
        Pk = 1234
    };


    [Test]
    public async Task LoginTest()
    {
        var userService = new UserService(_credentials,
            _uriProvider,
            _httpClient,
            _launcherKeysProvider,
            _deviceService,
            _converters,
            _userStateService,
            _apiStateProvider,
            _passwordEncryptor,
            new AuthorizationHeaderProvider());

        var result = await userService.LoginAsync();

        Assert.IsTrue(result.IsRight);
        Assert.IsNotNull(_currentUser);
        Assert.AreEqual(_testUser.Pk, _currentUser.Pk);
    }

    [Test]
    public async Task LogoutTest()
    {
        var userService = new UserService(_credentials,
            _uriProvider,
            _httpClient,
            _launcherKeysProvider,
            _deviceService,
            _converters,
            _userStateService,
            _apiStateProvider,
            _passwordEncryptor,
            new AuthorizationHeaderProvider());

        var result = await userService.LogoutAsync();

        Assert.IsTrue(result.IsRight);
        Assert.IsNull(_apiStateProvider.CurrentUser);
    }
}